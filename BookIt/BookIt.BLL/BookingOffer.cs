using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.BLL
{
    public class BookingOffer
    {
        public int Id { get; set; }
        public int? BookingSubjectId { get; set; }
        public List<BookingTimeSlot> TimeSlots { get; set; }
        public bool IsOccupied { get; set; }
        public Person Owner { get; set; }
        public string SubjectName { get; set; }
        public bool IsInfinite { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Сортирует промежутки времени, чтобы они шли по порядку, 
        /// объединяет соседние свободные промежутки, и соседние занатые промежутки, если они заняты одним и тем же пользователем
        /// </summary>
        public void NormalizeTimeSlots()
        {
#warning что-то тут работает криво
            List<BookingTimeSlot> slotsList = TimeSlots.OrderBy(ts => ts.StartDate).ToList();
            int count = slotsList.Count - 1;
            for (int i = 0; i < count; i++)
            {
                if (!slotsList[i].IsOccupied && !slotsList[i + 1].IsOccupied)
                {
                    slotsList[i].EndDate = slotsList[i + 1].EndDate;
                    slotsList.RemoveAt(i + 1);
                    count--;
                }
                else if (slotsList[i].IsOccupied && slotsList[i + 1].IsOccupied && slotsList[i].Person.Id == slotsList[i + 1].Person.Id)
                {
                    slotsList[i].EndDate = slotsList[i + 1].EndDate;
                    slotsList.RemoveAt(i + 1);
                    count--;
                }
            }
            TimeSlots = slotsList;
        }

        /// <summary>
        /// Бронировать конкретное предложение на указанный период
        /// </summary>
        /// <param name="startDate">Дата начала резервирования</param>
        /// <param name="endDate">Дата окончания резервирования</param>
        /// <returns>Признак, успешно забронировано или нет</returns>
        public bool Book(DateTime startDate, DateTime endDate, Person person)
        {
            //если тайм слотов еще не было, значит считаем, что полностью свободный на неограниченное время промежуток времени
            if (this.TimeSlots == null)
            {
                BookingTimeSlot ts = new BookingTimeSlot()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsOccupied = true
                };
                this.TimeSlots = new List<BookingTimeSlot>() { ts };
            }
            else
            {
                NormalizeTimeSlots();
                //находим промежуток времени, который влючает переданные даты
                BookingTimeSlot slot = this.TimeSlots.FirstOrDefault(ts => ts.StartDate.Date <= startDate.Date && ts.EndDate.Date >= endDate.Date && !ts.IsOccupied);
                if (slot == null)
                    //не найден промежуток времени, в который входит переданный, т.е. указанный промежуток времени не может быть забукан
                    return false;
#warning Вот тут не реализована ситуация, когда предмет бесконечный, но уже имеет занятый период. Передаем другой период для резервирования - такой сценарий не работает
                this.TimeSlots.AddRange(BookPartialTimeSlot(slot, startDate, endDate, person));
                NormalizeTimeSlots();
            }
            return true;
        }

        /// <summary>
        /// Обновляет полученное предложение для бронирования данными о предмете бронивания и времени
        /// </summary>
        /// <param name="subject"></param>
        public void FillFromSubject(BookingSubject subject)
        {
            CreateTimeSlot();
            BookingSubjectId = subject.Id;
            SubjectName = subject.Name;
        }

        public void FillCustomBookingOffer()
        {
            IsInfinite = false;
            CreateTimeSlot();
        }

        /// <summary>
        /// Забронировать частичный промежуток времени
        /// </summary>
        /// <param name="slot">Временной промежуток, который должен быть забронировал либо целиком (если интервалы времени совпадаеют), либо его часть </param>
        /// <param name="startDate">Start date of the booking interval</param>
        /// <param name="endDate">End date of the booking interval</param>
        /// <returns></returns>
        private List<BookingTimeSlot> BookPartialTimeSlot(BookingTimeSlot slot, DateTime startDate, DateTime endDate, Person person)
        {
            if (slot.StartDate > startDate || slot.EndDate < endDate || slot.IsOccupied)
            {
                throw new ArgumentException("You try to book not accessible time interval");
            }

            List<BookingTimeSlot> splitedList = new List<BookingTimeSlot>();
            startDate = startDate.Date;
            endDate = endDate.Date;

            //create occupied slot
            splitedList.Add(new BookingTimeSlot()
            {
                IsOccupied = true,
                StartDate = startDate,
                EndDate = endDate,
                BookingOfferId = slot.BookingOfferId,
                Person = person
            });

            if (slot.StartDate == startDate && slot.EndDate == endDate)
            {
                //we have only one interval that equals free time slot 
                return splitedList;
            }
            else if (slot.StartDate == startDate)
            {
                //the booked interval start date is in the begining of the free time slot
                splitedList.Add(new BookingTimeSlot()
                {
                    IsOccupied = false,
                    StartDate = endDate.AddDays(1),
                    EndDate = slot.EndDate,
                    BookingOfferId = slot.BookingOfferId,
                });
            }
            else if (slot.EndDate == endDate)
            {
                //the booked interval end date is in the end of the free time slot
                splitedList.Add(new BookingTimeSlot()
                {
                    IsOccupied = false,
                    StartDate = slot.StartDate,
                    EndDate = endDate.AddDays(-1),
                    BookingOfferId = slot.BookingOfferId,
                });
            }
            else
            {
                //we are booking interval in the middle of free one

                //free interval at the beginning of the slot
                splitedList.Add(new BookingTimeSlot()
                {
                    IsOccupied = false,
                    StartDate = slot.StartDate,
                    EndDate = startDate.AddDays(-1),
                    BookingOfferId = slot.BookingOfferId,
                });

                //free interval at the end of the slot
                splitedList.Add(new BookingTimeSlot()
                {
                    IsOccupied = false,
                    StartDate = endDate.AddDays(1),
                    EndDate = slot.EndDate,
                    BookingOfferId = slot.BookingOfferId,
                });
            }
            return splitedList;
        }

        private void CreateTimeSlot()
        {
            if (!IsInfinite && (!StartDate.HasValue || !EndDate.HasValue))
            {
                throw new ArgumentException("This offer is not infinite, but has not start and end dates.");
            }
            if (!IsInfinite)
            {
                //создаем свободный слот 
                BookingTimeSlot slot = new BookingTimeSlot()
                {
                    IsOccupied = false,
                    StartDate = StartDate.Value,
                    EndDate = EndDate.Value,
                    Person = null //пока ничей
                };
                TimeSlots = new List<BookingTimeSlot>() { slot };
            }
        }
    }
}
