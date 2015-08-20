using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;

namespace BookIt.BLL.Services
{
    public class BookingService : IBookingService
    {
        public bool Book(BookingOfferDto bookingOfferDto, int slotId, DateTime startDate, DateTime endDate, UserDto person)
        {
            if (!bookingOfferDto.IsInfinite)
            {
                BookingTimeSlotDto slot = bookingOfferDto.TimeSlots.FirstOrDefault(ts => ts.Id == slotId);
                if (slot != null)
                {
                    if (slot.IsOccupied)
                        return false;
                    return BookTimeSlot(bookingOfferDto, slot, startDate, endDate, person);
                }
                else
                {
                    return Book(bookingOfferDto, startDate, endDate, person);
                }
            }
            return false;
        }

        /// <summary>
        /// Бронировать конкретное предложение на указанный период
        /// </summary>
        /// <param name="startDate">Дата начала резервирования</param>
        /// <param name="endDate">Дата окончания резервирования</param>
        /// <returns>Признак, успешно забронировано или нет</returns>
        public bool Book(BookingOfferDto bookingOfferDto, DateTime startDate, DateTime endDate, UserDto person)
        {
            //if not book, for example
            if (!bookingOfferDto.IsInfinite)
            {
                //находим промежуток времени, который влючает переданные даты
                BookingTimeSlotDto slot = bookingOfferDto.TimeSlots.FirstOrDefault(ts => ts.StartDate.Date <= startDate.Date && ts.EndDate.Date >= endDate.Date && !ts.IsOccupied);
                if (slot == null)
                    return false;
                else
                {
                    return BookTimeSlot(bookingOfferDto, slot, startDate, endDate, person);
                }

            }
            else
            {
#warning не сделано еще
                throw new NotImplementedException("You can implement this case here");
            }

        }

        public bool UnBook(BookingOfferDto bookingOfferDto, int slotId, UserDto person)
        {
            if (bookingOfferDto.TimeSlots.Count == 0)
                throw new ArgumentException("This offer doesn't contain slots");
            else
            {
                BookingTimeSlotDto slot = bookingOfferDto.TimeSlots.FirstOrDefault(ts => ts.Id.Equals(slotId));
                if (slot != null && CanPersonUnbookIt(slot, person))
                {
                    return UnBookTimeSlot(bookingOfferDto, slot, person);
                }
                else
                {
#warning need to return typified exception
                    return false;
                }

            }
        }

        private bool CanPersonUnbookIt(BookingTimeSlotDto slot, UserDto person)
        {
            return slot.Person.Id == person.Id;
        }

#warning нужно проверить
        private bool UnBookTimeSlot(BookingOfferDto bookingOfferDto, BookingTimeSlotDto slot, UserDto person)
        {
            var leftSlot = bookingOfferDto.TimeSlots.FirstOrDefault(ts => !ts.IsOccupied && ts.EndDate.Equals(slot.StartDate.AddDays(-1)));
            var rightSlot = bookingOfferDto.TimeSlots.FirstOrDefault(ts => !ts.IsOccupied && ts.StartDate.Equals(slot.EndDate.AddDays(1)));
            if (leftSlot == null && rightSlot == null)
            {
                slot.IsOccupied = false;
                slot.Person = null;
                return true;
            }

            if (leftSlot == null)//слева занято, а справа свободно, увеличиваем правый слот
            {
                rightSlot.StartDate = slot.StartDate;
                bookingOfferDto.TimeSlots.Remove(slot);
                return true;
            }
            if (rightSlot == null)
            {
                leftSlot.EndDate = slot.EndDate;
                bookingOfferDto.TimeSlots.Remove(slot);
                return true;
            }
            leftSlot.EndDate = rightSlot.EndDate;
            bookingOfferDto.TimeSlots.Remove(slot);
            bookingOfferDto.TimeSlots.Remove(rightSlot);
            return true;


        }

        /// <summary>
        /// Обновляет полученное предложение для бронирования данными о предмете бронивания и времени
        /// </summary>
        /// <param name="subject"></param>
        public void FillFromSubject(BookingOfferDto bookingOfferDto, BookingSubjectDto subject)
        {
            CreateTimeSlot(bookingOfferDto);
            bookingOfferDto.BookingSubjectId = subject.Id;
            bookingOfferDto.Name = subject.Name;
            bookingOfferDto.Category = subject.Category;
        }

        public void FillCustomBookingOffer(BookingOfferDto bookingOfferDto)
        {
            bookingOfferDto.IsInfinite = false;
            CreateTimeSlot(bookingOfferDto);
        }

        /// <summary>
        /// Books the time slots.
        /// </summary>
        /// <param name="slot">The slot.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="person">The person.</param>
        private bool BookTimeSlot(BookingOfferDto bookingOfferDto, BookingTimeSlotDto slot, DateTime startDate, DateTime endDate, UserDto person)
        {
            if (slot.StartDate > startDate || slot.EndDate < endDate || slot.IsOccupied)
            {
                throw new ArgumentException("You try to book not accessible time interval");
            }

            if (slot.StartDate == startDate && slot.EndDate == endDate)
            {
                slot.IsOccupied = true;
                slot.Person = person;
                return true;
            }

            var busySlot = new BookingTimeSlotDto()
            {
                StartDate = startDate,
                EndDate = endDate,
                IsOccupied = true,
                BookingOfferId = slot.BookingOfferId,//we don't need this fiels in our objects model
                Person = person
            };

            if (slot.StartDate == startDate)
            {
                slot.StartDate = endDate.AddDays(1);//next day


            }
            else if (slot.EndDate == endDate)
            {
                slot.EndDate = startDate.AddDays(-1);

            }
            else if (slot.StartDate != startDate && slot.EndDate != endDate)
            {
                var newFreeSlot = new BookingTimeSlotDto()
                {
                    StartDate = endDate.AddDays(1),
                    EndDate = slot.EndDate,
                    IsOccupied = false,
                    BookingOfferId = slot.BookingOfferId,
                };
                slot.EndDate = startDate.AddDays(-1);
                bookingOfferDto.TimeSlots.Add(newFreeSlot);

            }
            bookingOfferDto.TimeSlots.Add(busySlot);//SortedSet имеет особенность - в него нельзя добавлять поля с одинаковыми ключевыми элементами
            return true; ;
        }



        private void CreateTimeSlot(BookingOfferDto bookingOfferDto)
        {
            if (!bookingOfferDto.IsInfinite && (!bookingOfferDto.StartDate.HasValue || !bookingOfferDto.EndDate.HasValue))
            {
                throw new ArgumentException("This offer is not infinite, but has not start and end dates.");
            }
            if (!bookingOfferDto.IsInfinite)
            {
                //создаем свободный слот 
                BookingTimeSlotDto slot = new BookingTimeSlotDto()
                {
                    IsOccupied = false,
                    StartDate = bookingOfferDto.StartDate.Value,
                    EndDate = bookingOfferDto.EndDate.Value,
                    Person = null //пока ничей
                };
                bookingOfferDto.TimeSlots.Add(slot);
            }
        }
	}
}
