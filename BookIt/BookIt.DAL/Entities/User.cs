using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookIt.DAL.Entities
{
    public class User : IEntity
	{
		public int ID { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		public int RoleID { get; set; }
		[ForeignKey("RoleID")]
		public virtual Role Role { get; set; }

		public virtual ICollection<TimeSlot> TimeSlots { get; set; }

		public virtual ICollection<BookingSubject> BookingSubjects { get; set; }

		public virtual ICollection<BookingOffer> BookingOffers { get; set; }
	}
}
