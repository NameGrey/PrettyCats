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
        static List<BookingSubjectDto> bookingSubjects = new List<BookingSubjectDto>() 
        { 
            new BookingSubjectDto { Id = 1, Category = CategoryTypes.Other, Name = "Объект 1"}, 
            new BookingSubjectDto { Id = 2, Category = CategoryTypes.Sport, Name = "Объект 2",}, 
            new BookingSubjectDto { Id = 3, Category = CategoryTypes.Users, Name = "Объект 3" } 
        };

        static List<UserDto> persons = new List<UserDto>()
        { 
            new UserDto { Id = 1, FirstName = "Иван", LastName="Иванов", Role = RoleTypes.Administrator }, 
            new UserDto { Id = 2, FirstName = "Иван", LastName="Петров", Role = RoleTypes.User }, 
            new UserDto { Id = 3, FirstName = "Светлана", LastName="Пахомова", Role = RoleTypes.User }, 
          
        };

        static List<BookingOfferDto> offers = new List<BookingOfferDto>()
        { 
            new BookingOfferDto() { Id = 1, BookingSubjectId = 2, EndDate = new DateTime(2012, 12, 12), IsInfinite = false, IsOccupied = false, Owner = persons[0], StartDate = new DateTime(2012, 11, 12), Name = "Зачем он вообще?", TimeSlots = null},
        };

        static List<BookingTimeSlotDto> slots = new List<BookingTimeSlotDto>()
        { 
          
        };


        static public List<BookingSubjectDto> GetAllBookingSubjects() 
        { 
            return bookingSubjects;
        }

        static public List<BookingOfferDto> GetAllBookingOffers()
        {
            return offers;
        }

        static public List<BookingOfferDto> GetFreeBookingOffers()
        {
            return offers.Where(s => s.IsOccupied == false).ToList();
        }

        static public List<UserDto> GetPersons()
        {
            return persons;
        }

        static public BookingTimeSlotDto SaveBookingTimeSlot(BookingTimeSlotDto slot)
        {
            int maxId = slots.Any() ? slots.Max(s => s.Id) + 1 : 1;
            slot.Id = maxId;
            slots.Add(slot);
            return slot;
        }

        static public BookingOfferDto SaveBookingOffer(BookingOfferDto bookingOffer)
        {
            if (bookingOffer.Id == default(int))
            {
                int maxOfferId = offers.Any() ? offers.Max(s => s.Id) + 1 : 1;
                bookingOffer.Id = maxOfferId;
                offers.Add(bookingOffer);
            }
            return bookingOffer;
        }

		static public BookingSubjectDto SaveBookingSubject(BookingSubjectDto bookingSubject)
		{
			if (bookingSubject.Id == default(int))
			{
				int maxSubjectId = bookingSubjects.Any() ? bookingSubjects.Max(s => s.Id) + 1 : 1;
				bookingSubject.Id = maxSubjectId;
				bookingSubjects.Add(bookingSubject);
			}
			return bookingSubject;
		}

        static public void UpdateBookingOffer(BookingOfferDto bookingOffer)
        {
            BookingOfferDto existedOffer = offers.FirstOrDefault(o => o.Id == bookingOffer.Id);
            slots.RemoveAll(s => s.BookingOfferId == bookingOffer.Id);
            if (bookingOffer.TimeSlots != null)
                foreach (BookingTimeSlotDto s in bookingOffer.TimeSlots)
                {
                    SaveBookingTimeSlot(s);
                }
            existedOffer.TimeSlots = bookingOffer.TimeSlots;
        }

    }
}
