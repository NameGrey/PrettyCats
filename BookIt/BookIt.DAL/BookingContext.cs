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
		public DbSet<Category> Categories { get; set; }
		public DbSet<Role> Roles { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<BookingSubject> BookingSubjects { get; set; }
		public DbSet<BookingOffer> BookingOffers { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }


		public BookingContext()
			: base("BookItDB")
		{
			Database.SetInitializer<BookingContext>(new BookingDBInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<TimeSlot>().HasOptional(t => t.Owner).WithMany().WillCascadeOnDelete(false);
			base.OnModelCreating(modelBuilder);
		}
	}
}

