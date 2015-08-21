using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingOffer : EntityBase
	{
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
