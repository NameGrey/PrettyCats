using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookIt.DAL.Entities
{
	public class TimeSlot
	{
		public int ID { get; set; }

		public bool IsOccupied { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		
		public int? OwnerID { get; set; }
		[ForeignKey("OwnerID")]
		public virtual User Owner { get; set; }
		
		public int BookingOfferID { get; set; }
		[ForeignKey("BookingOfferID")]
		public virtual BookingOffer BookingOffer { get; set; }
	}
}
