using System;
using System.Collections.Generic;

namespace BookIt.DAL
{
	public class Person
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Role Role { get; set; }

		public virtual ICollection<TimeSlot> TimeSlots { get; set; }

		public virtual ICollection<BookingSubject> BookingSubjects { get; set; }

		public virtual ICollection<BookingOffer> BookingOffers { get; set; }
	}
}
