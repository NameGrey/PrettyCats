using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookIt.DAL.Entities
{
    public class TimeSlot : IEntity
	{
		public int ID { get; set; }

		public bool IsOccupied { get; set; }
        [Column(TypeName = "Date")]
		public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

		
		public int? OwnerID { get; set; }
		[ForeignKey("OwnerID")]
		public virtual User Owner { get; set; }
		
		public int BookingOfferID { get; set; }
		[ForeignKey("BookingOfferID")]
		public virtual BookingOffer BookingOffer { get; set; }
	}
}
