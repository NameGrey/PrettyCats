using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class TimeSlot
	{
		public int ID { get; set; }

		public bool IsBusy { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int OwnerID { get; set; }
		[ForeignKey("OwnerID")]
		public virtual Person Owner { get; set; }
		
		public int BookingOfferID { get; set; }
		[ForeignKey("BookingOfferID")]
		public virtual BookingOffer BookingOffers { get; set; }
	}
}
