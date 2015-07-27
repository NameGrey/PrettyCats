using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingContext:DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<BookingSubject> BookingSubjects { get; set; }
		public DbSet<BookingOffer> BookingOffers { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }
	}
}

