using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL.Entities;

namespace BookIt.DAL
{
	public class BookingDBInitializer : CreateDatabaseIfNotExists<BookingContext>
	{
		protected override void Seed(BookingContext context)
		{
			FillTestData(context);
			base.Seed(context);
		}

		private void FillTestData(BookingContext context)
		{
			IList<Role> roles = new List<Role>();
			roles.Add(new Role{ID = 1, Name = "Administrator", Description = "User with this role can do anything"});
			roles.Add(new Role { ID = 2, Name = "Employee", Description = "User with this role cannot modify subjects" });
			context.Roles.AddRange(roles);

			IList<Category> categories = new List<Category>();
			categories.Add(new Category { ID = 1, Name = "Sport", Description = "Everything that concirns sport" });
			categories.Add(new Category { ID = 2, Name = "Parking", Description = "It is mostrly about parking places" });
			categories.Add(new Category { ID = 3, Name = "Books", Description = "Books and magazines" });
			categories.Add(new Category { ID = 4, Name = "Other", Description = "It is the place for any other kinds of things" });
			categories.Add(new Category { ID = 5, Name = "User", Description = "Things offered by user" });
			context.Categories.AddRange(categories);


			IList<User> defaultPersons = new List<User>();

			defaultPersons.Add(new User() { ID = 1, FirstName = "TestUser1", RoleID = 1, Email = "elena_ilyina@epam.com" });
			defaultPersons.Add(new User() { ID = 2, FirstName = "TestUser2", RoleID = 2, Email = "elena_ilyina@epam.com" });
			defaultPersons.Add(new User() { ID = 3, FirstName = "TestUser3", RoleID = 2, Email = "elena_ilyina@epam.com" });

			context.Users.AddRange(defaultPersons);

			IList<BookingSubject> defaultSubjects = new List<BookingSubject>();
			defaultSubjects.Add(new BookingSubject() { ID = 1, Name = "Bicycle", Description = "Atemi 20", Capacity = 3, OwnerID = 1, CategoryID = 1 });
			defaultSubjects.Add(new BookingSubject() { ID = 2, Name = "Parking Place 1", Description = "Near office", Capacity = 1, OwnerID = 2, CategoryID = 2 });
			defaultSubjects.Add(new BookingSubject() { ID = 3, Name = "Parking Place 2", Description = "Near office", Capacity = 1, OwnerID = 3, CategoryID = 2 });

			context.BookingSubjects.AddRange(defaultSubjects);

			var startDate = DateTime.Now;
			var endDate = startDate.AddDays(14);

			var startDate2 = endDate.AddDays(1);
			var endDate2 = startDate2.AddMonths(3);
			IList<BookingOffer> defaultOffers = new List<BookingOffer>();

			defaultOffers.Add(new BookingOffer() { ID = 1, BookingSubjectID = 1, Name = "Bicycle", CategoryID = 1, IsInfinite = false, Description = "RED", StartDate = startDate, EndDate = endDate, OwnerID = 1 });
			defaultOffers.Add(new BookingOffer() { ID = 2, BookingSubjectID = 1, Name = "Bicycle", CategoryID = 1, IsInfinite = false, Description = "Blue", StartDate = startDate, EndDate = endDate, OwnerID = 1 });
			defaultOffers.Add(new BookingOffer() { ID = 3, BookingSubjectID = 1, Name = "Bicycle", CategoryID = 1, IsInfinite = false, Description = "White", StartDate = startDate, EndDate = endDate, OwnerID = 1 });

			defaultOffers.Add(new BookingOffer() { ID = 4, BookingSubjectID = 2, Name = "Parking Place 1", CategoryID = 2, IsInfinite = false, Description = "illness", StartDate = startDate.AddDays(-3), EndDate = endDate, OwnerID = 2 });
			defaultOffers.Add(new BookingOffer() { ID = 5, BookingSubjectID = 2, Name = "Parking Place 1", CategoryID = 2, IsInfinite = false, Description = "vacation", StartDate = startDate2, EndDate = endDate2, OwnerID = 2 });

			defaultOffers.Add(new BookingOffer() { ID = 7, BookingSubjectID = 3, Name = "Parking Place 2",  CategoryID = 2, IsInfinite = false, Description = "illness", StartDate = startDate, EndDate = endDate, OwnerID = 3 });
			defaultOffers.Add(new BookingOffer() { ID = 8, BookingSubjectID = 3, Name = "Parking Place 2",  CategoryID = 2, IsInfinite = false, Description = "vacation", StartDate = startDate2, EndDate = endDate2, OwnerID = 3 });

			defaultOffers.Add(new BookingOffer() { ID = 6, Name = "Majhong game", CategoryID = 5, IsInfinite = false, Description = "chinese table game", StartDate = startDate, EndDate = endDate, OwnerID = 3 });

			context.BookingOffers.AddRange(defaultOffers);

			IList<TimeSlot> defaultTimeSlots = new List<TimeSlot>();

			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 1, StartDate = startDate, EndDate = endDate, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 2, StartDate = startDate, EndDate = endDate, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 3, StartDate = startDate, EndDate = endDate, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 4, StartDate = startDate, EndDate = endDate, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 5, StartDate = startDate2, EndDate = endDate2, IsOccupied = false });

			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 7, StartDate = startDate, EndDate = endDate, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 8, StartDate = startDate2, EndDate = endDate2, IsOccupied = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 6, StartDate = startDate, EndDate = endDate, IsOccupied = false });


			context.TimeSlots.AddRange(defaultTimeSlots);
		}
	}
}
