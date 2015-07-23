﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.BLL
{
    public class BookingOffer
    {
        public int Id { get; set; }
        public int? BookingSubjectId { get; set; }
		public SortedSet<BookingTimeSlot> TimeSlots { get; set; }
        public bool IsOccupied { get; set; }
        public Person Owner { get; set; }
        public string SubjectName { get; set; }
        public bool IsInfinite { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

		public BookingOffer()
		{
			this.TimeSlots = new SortedSet<BookingTimeSlot>(new BookingTimeSlotComparer());

		}

        /// <summary>
        /// Сортирует промежутки времени, чтобы они шли по порядку, 
        /// объединяет соседние свободные промежутки, и соседние занатые промежутки, если они заняты одним и тем же пользователем
        /// </summary>
//		private void NormalizeTimeSlots()
//		{
//#warning что-то тут работает криво
//			List<BookingTimeSlot> slotsList = TimeSlots.OrderBy(ts => ts.StartDate).ToList();
//			int count = slotsList.Count - 1;
//			for (int i = 0; i < count; i++)
//			{
//				if (!slotsList[i].IsOccupied && !slotsList[i + 1].IsOccupied)
//				{
//					slotsList[i].EndDate = slotsList[i + 1].EndDate;
//					slotsList.RemoveAt(i + 1);
//					count--;
//				}
//				else if (slotsList[i].IsOccupied && slotsList[i + 1].IsOccupied && slotsList[i].Person.Id == slotsList[i + 1].Person.Id)
//				{
//					slotsList[i].EndDate = slotsList[i + 1].EndDate;
//					slotsList.RemoveAt(i + 1);
//					count--;
//				}
//			}
//			TimeSlots = slotsList;
//		}

        /// <summary>
        /// Бронировать конкретное предложение на указанный период
        /// </summary>
        /// <param name="startDate">Дата начала резервирования</param>
        /// <param name="endDate">Дата окончания резервирования</param>
        /// <returns>Признак, успешно забронировано или нет</returns>
        public bool Book(DateTime startDate, DateTime endDate, Person person)
        {
            //если тайм слотов еще не было, значит считаем, что полностью свободный на неограниченное время промежуток времени
			if (this.TimeSlots.Count == 0)
			{
				BookingTimeSlot ts = new BookingTimeSlot()
				{
					StartDate = startDate,
					EndDate = endDate,
					IsOccupied = true
				};
			}
			else
			{
				//if not book, for example
				if (!this.IsInfinite)
				{
     				//находим промежуток времени, который влючает переданные даты
					BookingTimeSlot slot = this.TimeSlots.FirstOrDefault(ts => ts.StartDate.Date <= startDate.Date && ts.EndDate.Date >= endDate.Date && !ts.IsOccupied);
					if (slot == null)
						return false;
					else
					{
						return BookTimeSlot(slot, startDate, endDate, person);
					}

				}
				else
				{
#warning не сделано еще
					throw new NotImplementedException("You can implement this case here");
				}
			}
            return false;
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
		/// Books the time slots.
		/// </summary>
		/// <param name="slot">The slot.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <param name="person">The person.</param>
		private bool BookTimeSlot(BookingTimeSlot slot, DateTime startDate, DateTime endDate, Person person)
		{
			if (slot.StartDate > startDate || slot.EndDate < endDate || slot.IsOccupied)
			{
				throw new ArgumentException("You try to book not accessible time interval");
			}

			if (slot.StartDate==startDate&&slot.EndDate==endDate)
			{
				slot.IsOccupied = true;
				slot.Person = person;
				return true;
			}

			var busySlot = new BookingTimeSlot()
			{
				StartDate = startDate,
				EndDate = endDate,
				IsOccupied = true,
				BookingOfferId = slot.BookingOfferId,//we don't need this fiels in our objects model
				Person = person
			};

			this.TimeSlots.Add(busySlot);
			if (slot.StartDate == startDate)
			{
				slot.StartDate = endDate.AddDays(1);//next day
				return true;
				
			}else if (slot.EndDate == endDate)
			{
				slot.EndDate = startDate.AddDays(-1);
				return true;
			}else if (slot.StartDate != startDate && slot.EndDate != endDate)
			{
				var newFreeSlot = new BookingTimeSlot()
				{
					StartDate = endDate.AddDays(1),
					EndDate = slot.EndDate,
					IsOccupied = false,
					BookingOfferId = slot.BookingOfferId
				};
				slot.EndDate = startDate.AddDays(-1);
				this.TimeSlots.Add(newFreeSlot);
				return true;
			}

			return false; ;
		}

		private bool UnBookTimeSlot()
		{
			throw new NotImplementedException();
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
                TimeSlots.Add(slot);
            }
        }
    }
}
