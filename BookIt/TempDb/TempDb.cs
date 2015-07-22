using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using System.Collections;


namespace TempDatabase
{
    public class TempDb
    {
        static List<BookingSubject> bookingSubject = new List<BookingSubject>() 
        { 
            new BookingSubject { Id = 1, CategoryId = 1, Name = "Объект 1"}, 
            new BookingSubject { Id = 2, CategoryId = 1, Name = "Объект 2"}, 
            new BookingSubject { Id = 3, CategoryId = 2, Name = "Объект 3" } 
        };

        static List<Person> persons = new List<Person>()
        { 
            new Person { Id = 1, FirstName = "Иван", LastName="Иванов", PersonRole = Role.Administrator }, 
            new Person { Id = 2, FirstName = "Иван", LastName="Петров", PersonRole = Role.User }, 
            new Person { Id = 3, FirstName = "Светлана", LastName="Пахомова", PersonRole = Role.User }, 
          
        };

        static List<BookingOffer> offers = new List<BookingOffer>()
        { 
          
        };

        static List<BookingTimeSlot> slots = new List<BookingTimeSlot>()
        { 
          
        };


        static public List<BookingSubject> GetAllBookingSubjects() 
        { 
            return bookingSubject;
        }

        static public List<BookingOffer> GetAllBookingOffers()
        {
            return offers;
        }

        static public List<BookingOffer> GetFreeBookingOffers()
        {
            return offers.Where(s => s.IsOccupied == false).ToList();
        }

        static public List<Person> GetPersons()
        {
            return persons;
        }

        static public BookingTimeSlot SaveBookingTimeSlot(BookingTimeSlot slot)
        {
            int maxId = slots.Any() ? slots.Max(s => s.Id) + 1 : 1;
            slot.Id = maxId;
            slots.Add(slot);
            return slot;
        }

        static public BookingOffer SaveBookingOffer(BookingOffer bookingOffer)
        {
            if (bookingOffer.Id == default(int))
            {
                int maxOfferId = offers.Any() ? offers.Max(s => s.Id) + 1 : 1;
                bookingOffer.Id = maxOfferId;
                offers.Add(bookingOffer);
            }
            return bookingOffer;
        }

        static public void UpdateBookingOffer(BookingOffer bookingOffer)
        {
            BookingOffer existedOffer = offers.FirstOrDefault(o => o.Id == bookingOffer.Id);
            slots.RemoveAll(s => s.BookingOfferId == bookingOffer.Id);
            if (bookingOffer.TimeSlots != null)
                foreach (BookingTimeSlot s in bookingOffer.TimeSlots)
                {
                    SaveBookingTimeSlot(s);
                }
            existedOffer.TimeSlots = bookingOffer.TimeSlots;
        }

    }
}
