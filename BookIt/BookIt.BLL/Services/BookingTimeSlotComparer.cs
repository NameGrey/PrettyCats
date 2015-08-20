using System;
using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.BLL.Services
{
	public class BookingTimeSlotComparer : IComparer<BookingTimeSlotDto>
	{
		#region IComparer<BookingTimeSlot> Members

		public int Compare(BookingTimeSlotDto x, BookingTimeSlotDto y)
		{
			return Comparer<DateTime>.Default.Compare(x.StartDate, y.StartDate);
		}

		#endregion
	}
}
