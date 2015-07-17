using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookIt.BLL;

namespace BookIt.Repository
{
    public interface IBookItRepository
    {
        IEnumerable<Person> GetPersons();
        IEnumerable<BookingSubject> GetAllBookingSubjects();
        IEnumerable<BookingOffer> GetAllBookingOffers();
        void SaveBookingOffer(BookingOffer offer);
        void UpdateBookingOffer(BookingOffer bookingOffer);

    }
}
