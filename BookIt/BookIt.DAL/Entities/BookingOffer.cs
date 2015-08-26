using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookIt.DAL.Entities
{
    public class BookingOffer : IEntity
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public int CategoryID { get; set; }
		[ForeignKey("CategoryID")]
		public virtual Category Category { get; set; }
		
		public bool IsInfinite { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		
		public int OwnerID { get; set; }
		[ForeignKey("OwnerID")]
		public virtual User Owner { get; set; }

		public int? BookingSubjectID { get; set; }
		[ForeignKey("BookingSubjectID")]
		public virtual BookingSubject BookingSubject { get; set; }

		public virtual ICollection<TimeSlot> TimeSlots { get; set; }
	}
}
