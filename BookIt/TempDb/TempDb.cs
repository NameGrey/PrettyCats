using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using System.Collections;
using BookIt.BLL.Entities;


namespace TempDatabase
{
    public class TempDb
    {
		static List<Category> categoryDtos = new List<Category>() 
        { 
            new Category { Id = 1, Name = "Sport"}, 
			new Category { Id = 2, Name = "Users"}, 
			new Category { Id = 3, Name = "Other"}, 
        };

        static List<Subject> bookingSubjects = new List<Subject>() 
        { 
            new Subject { Id = 1, Category = new Category(){Id = 3, Name = "Other"}, Name = "Объект 1"}, 
            new Subject { Id = 2, Category = new Category(){Id = 2, Name = "Sport"}, Name = "Объект 2",}, 
            new Subject { Id = 3, Category = new Category(){Id = 1, Name = "User"}, Name = "Объект 3" } 
        };

        static List<User> persons = new List<User>()
        { 
            new User { Id = 1, FirstName = "Иван", LastName="Иванов", Role = new Role(){Id = 1, Name = "Administrator"} }, 
            new User { Id = 2, FirstName = "Иван", LastName="Петров", Role = new Role(){Id = 2, Name = "User"} }, 
            new User { Id = 3, FirstName = "Светлана", LastName="Пахомова", Role = new Role(){Id = 2, Name = "User"} }, 
          
        };

        static List<Offer> offers = new List<Offer>()
        { 
            new Offer() { Id = 1, BookingSubjectId = 2, EndDate = new DateTime(2012, 12, 12), IsInfinite = false, IsOccupied = false, Owner = persons[0], StartDate = new DateTime(2012, 11, 12), Name = "Зачем он вообще?", TimeSlots = null},
        };

        static List<TimeSlot> slots = new List<TimeSlot>()
        { 
          
        };


        static public List<Subject> GetAllBookingSubjects() 
        { 
            return bookingSubjects;
        }

        static public List<Offer> GetAllBookingOffers()
        {
            return offers;
        }

        static public List<Offer> GetFreeBookingOffers()
        {
            return offers.Where(s => s.IsOccupied == false).ToList();
        }

        static public List<User> GetPersons()
        {
            return persons;
        }

        static public TimeSlot SaveBookingTimeSlot(TimeSlot slot)
        {
            int maxId = slots.Any() ? slots.Max(s => s.Id) + 1 : 1;
            slot.Id = maxId;
            slots.Add(slot);
            return slot;
        }

        static public Offer SaveBookingOffer(Offer bookingOffer)
        {
            if (bookingOffer.Id == default(int))
            {
                int maxOfferId = offers.Any() ? offers.Max(s => s.Id) + 1 : 1;
                bookingOffer.Id = maxOfferId;
                offers.Add(bookingOffer);
            }
            return bookingOffer;
        }

		static public Subject SaveBookingSubject(Subject bookingSubject)
		{
			if (bookingSubject.Id == default(int))
			{
				int maxSubjectId = bookingSubjects.Any() ? bookingSubjects.Max(s => s.Id) + 1 : 1;
				bookingSubject.Id = maxSubjectId;
				bookingSubjects.Add(bookingSubject);
			}
			return bookingSubject;
		}

        static public void UpdateBookingOffer(Offer bookingOffer)
        {
            Offer existedOffer = offers.FirstOrDefault(o => o.Id == bookingOffer.Id);
            slots.RemoveAll(s => s.BookingOfferId == bookingOffer.Id);
            if (bookingOffer.TimeSlots != null)
                foreach (TimeSlot s in bookingOffer.TimeSlots)
                {
                    SaveBookingTimeSlot(s);
                }
            existedOffer.TimeSlots = bookingOffer.TimeSlots;
        }

    }
}
