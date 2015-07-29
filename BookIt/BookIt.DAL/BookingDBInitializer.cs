using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingDBInitializer : CreateDatabaseIfNotExists<BookingContext>
	{
		protected override void Seed(BookingContext context)
		{
			IList<Person> defaultPersons = new List<Person>();
			
			defaultPersons.Add(new Person() {ID=1, FirstName = "TestUser1",Role=Role.Administrator });
			defaultPersons.Add(new Person() {ID=2, FirstName = "TestUser2", Role = Role.Employee });
			defaultPersons.Add(new Person() {ID=3, FirstName = "TestUser3", Role = Role.Employee });

			context.Persons.AddRange(defaultPersons);
			
			IList<BookingSubject> defaultSubjects = new List<BookingSubject>();
			defaultSubjects.Add(new BookingSubject() {ID=1, Name="Bicycle", Description="Atemi 20",Count=3,OwnerID=1 , Category=Category.Sport});
			defaultSubjects.Add(new BookingSubject() { ID = 2, Name = "Parking Place 1", Description = "Near office", Count = 1, OwnerID = 2, Category = Category.Parking });
			defaultSubjects.Add(new BookingSubject() { ID = 3, Name = "Parking Place 2", Description = "Near office", Count = 1, OwnerID = 3, Category = Category.Parking });

			context.BookingSubjects.AddRange(defaultSubjects);
			
			var startDate = DateTime.Now;
			var endDate = startDate.AddMonths(3);

			IList<BookingOffer> defaultOffers = new List<BookingOffer>();

			defaultOffers.Add(new BookingOffer() {ID=1, BookingSubjectID = 1, Name = "Bicycle", Category=Category.Sport, IsInfinite=false,Description="RED", StartDate=startDate,EndDate=endDate, OwnerID=1 });
			defaultOffers.Add(new BookingOffer() { ID = 2, BookingSubjectID = 1, Name = "Bicycle", Category = Category.Sport, IsInfinite = false, Description = "Blue", StartDate = startDate, EndDate = endDate, OwnerID = 1 });
			defaultOffers.Add(new BookingOffer() { ID = 3, BookingSubjectID = 1, Name = "Bicycle", Category = Category.Sport, IsInfinite = false, Description = "White", StartDate = startDate, EndDate = endDate, OwnerID = 1 });

			defaultOffers.Add(new BookingOffer() { ID = 4, BookingSubjectID = 2, Name = "Parking Place 1", Category = Category.Parking, IsInfinite = false, Description = "illness", StartDate = startDate, EndDate = endDate, OwnerID = 2 });
			defaultOffers.Add(new BookingOffer() { ID = 5, BookingSubjectID = 2, Name = "Parking Place 2", Category = Category.Parking, IsInfinite = false, Description = "vacation", StartDate = startDate, EndDate = endDate, OwnerID = 3 });

			defaultOffers.Add(new BookingOffer() { ID = 6,  Name = "Majhong game", Category = Category.Users, IsInfinite = false, Description = "chinese table game", StartDate = startDate, EndDate = endDate, OwnerID = 3 });

			context.BookingOffers.AddRange(defaultOffers);

			IList<TimeSlot> defaultTimeSlots = new List<TimeSlot>();

			defaultTimeSlots.Add(new TimeSlot() {BookingOfferID=1,StartDate=startDate, EndDate=endDate,IsBusy=false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 2, StartDate = startDate, EndDate = endDate, IsBusy = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 3, StartDate = startDate, EndDate = endDate, IsBusy = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 4, StartDate = startDate, EndDate = endDate, IsBusy = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 5, StartDate = startDate, EndDate = endDate, IsBusy = false });
			defaultTimeSlots.Add(new TimeSlot() { BookingOfferID = 6, StartDate = startDate, EndDate = endDate, IsBusy = false });
			
			context.TimeSlots.AddRange(defaultTimeSlots);

			base.Seed(context);
		}
	}
}
