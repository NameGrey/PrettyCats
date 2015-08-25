using System;
using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.BLL.Services
{
	public class BookingTimeSlotComparer : IComparer<TimeSlot>
	{
		#region IComparer<BookingTimeSlot> Members

		public int Compare(TimeSlot x, TimeSlot y)
		{
			return Comparer<DateTime>.Default.Compare(x.StartDate, y.StartDate);
		}

		#endregion
	}
}
