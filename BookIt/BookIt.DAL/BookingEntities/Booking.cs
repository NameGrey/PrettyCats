using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class Booking
	{
		public Guid ID { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int UserID { get; set; }

		public virtual User User { get; set; }

		public Guid GoodBookingItemID { get; set; }

		public virtual GoodBookingItem GoodBookingItem {get;set;}
		
	}
}
