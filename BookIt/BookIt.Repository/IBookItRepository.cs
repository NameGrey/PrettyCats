using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookIt.BLL;
using BookIt.BLL.Entities;

namespace BookIt.Repository
{
    public interface IBookItRepository
    {
		IEnumerable<CategoryTypes> GetCategories();
        IEnumerable<UserDto> GetPersons();
        IEnumerable<BookingSubjectDto> GetAllBookingSubjects();
        IEnumerable<BookingOfferDto> GetAllBookingOffers();
        void CreateBookingOffer(BookingOfferDto offer);
		void CreateBookingSubject(BookingSubjectDto subject);
        void UpdateBookingOffer(BookingOfferDto bookingOffer);

    }
}
