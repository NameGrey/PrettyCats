using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Services;

namespace BookIt.BLL.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public int? BookingSubjectId { get; set; }
        public SortedSet<TimeSlot> TimeSlots { get; set; }
        public bool IsOccupied { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInfinite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Category Category { get; set; }

        public Offer()
        {
            TimeSlots = new SortedSet<TimeSlot>(new BookingTimeSlotComparer());
        }


		public bool Book(int slotId, DateTime startDate, DateTime endDate, User user)
		{
			if (!IsInfinite)
			{
				TimeSlot slot = TimeSlots.FirstOrDefault(ts => ts.Id == slotId);
				if (slot != null)
				{
					if (slot.IsOccupied)
						return false;
					return BookTimeSlot(slot, startDate, endDate, user);
				}
				else
				{
					return Book(startDate, endDate, user);
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
		public bool Book(DateTime startDate, DateTime endDate, User user)
		{
			//if not book, for example
			if (!IsInfinite)
			{
				//находим промежуток времени, который влючает переданные даты
				TimeSlot slot = TimeSlots.FirstOrDefault(ts => ts.StartDate.Date <= startDate.Date && ts.EndDate.Date >= endDate.Date && !ts.IsOccupied);
				if (slot == null)
					return false;
				else
				{
					return BookTimeSlot(slot, startDate, endDate, user);
				}

			}
			else
			{
#warning не сделано еще
				throw new NotImplementedException("You can implement this case here");
			}

		}

		public bool UnBook(int slotId, User user)
		{
			if (TimeSlots.Count == 0)
				throw new ArgumentException("This offer doesn't contain slots");
			else
			{
				TimeSlot slot = TimeSlots.FirstOrDefault(ts => ts.Id.Equals(slotId));
				if (slot != null && CanUserUnbookIt(slot, user))
				{
					return UnBookTimeSlot(slot);
				}
				else
				{
#warning need to return typified exception
					return false;
				}

			}
		}

		private bool CanUserUnbookIt(TimeSlot slot, User user)
		{
			return slot.Owner.Id == user.Id;
		}

#warning нужно проверить
		private bool UnBookTimeSlot(TimeSlot slot)
		{
			var leftSlot = TimeSlots.FirstOrDefault(ts => !ts.IsOccupied && ts.EndDate.Equals(slot.StartDate.AddDays(-1)));
			var rightSlot = TimeSlots.FirstOrDefault(ts => !ts.IsOccupied && ts.StartDate.Equals(slot.EndDate.AddDays(1)));
			if (leftSlot == null && rightSlot == null)
			{
				slot.IsOccupied = false;
				slot.Owner = null;
				return true;
			}

			if (leftSlot == null)//слева занято, а справа свободно, увеличиваем правый слот
			{
				rightSlot.StartDate = slot.StartDate;
				TimeSlots.Remove(slot);
				return true;
			}
			if (rightSlot == null)
			{
				leftSlot.EndDate = slot.EndDate;
				TimeSlots.Remove(slot);
				return true;
			}
			leftSlot.EndDate = rightSlot.EndDate;
			TimeSlots.Remove(slot);
			TimeSlots.Remove(rightSlot);
			return true;


		}

		/// <summary>
		/// Обновляет полученное предложение для бронирования данными о предмете бронивания и времени
		/// </summary>
		/// <param name="subject"></param>
		public void FillFromSubject(Subject subject)
		{
			CreateTimeSlot();
			BookingSubjectId = subject.Id;
			Name = subject.Name;
			Category = subject.Category;
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
		/// <param name="user">The person.</param>
		private bool BookTimeSlot(TimeSlot slot, DateTime startDate, DateTime endDate, User user)
		{
			if (slot.StartDate > startDate || slot.EndDate < endDate || slot.IsOccupied)
			{
				throw new ArgumentException("You try to book not accessible time interval");
			}

			if (slot.StartDate == startDate && slot.EndDate == endDate)
			{
				slot.IsOccupied = true;
				slot.Owner = user;
				return true;
			}

			var busySlot = new TimeSlot()
			{
				StartDate = startDate,
				EndDate = endDate,
				IsOccupied = true,
				BookingOfferId = Id,
				Owner = user
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
				var newFreeSlot = new TimeSlot()
				{
					StartDate = endDate.AddDays(1),
					EndDate = slot.EndDate,
					IsOccupied = false,
					BookingOfferId = Id
				};
				slot.EndDate = startDate.AddDays(-1);
				TimeSlots.Add(newFreeSlot);

			}
			TimeSlots.Add(busySlot);//SortedSet имеет особенность - в него нельзя добавлять поля с одинаковыми ключевыми элементами
			return true; ;
		}



		private void CreateTimeSlot()
		{
			if (!IsInfinite && !EndDate.HasValue)
			{
				throw new ArgumentException("This offer is not infinite, but has not end date.");
			}
			if (!IsInfinite)
			{
				//создаем свободный слот 
				TimeSlot slot = new TimeSlot()
				{
					IsOccupied = false,
					StartDate = StartDate,
					EndDate = EndDate.GetValueOrDefault(),
					Owner = null //пока ничей
				};
				TimeSlots.Add(slot);
			}
		}
    }
}
