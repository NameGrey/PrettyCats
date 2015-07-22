using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookIt.BLL
{
	public class BookingTimeSlotComparer : IComparer<BookingTimeSlot>
	{


		#region IComparer<BookingTimeSlot> Members

		public int Compare(BookingTimeSlot x, BookingTimeSlot y)
		{
			return Comparer<DateTime>.Default.Compare(x.StartDate, y.StartDate);
		}

		#endregion
	}
}
