using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using TempDatabase;

namespace BookIt.Repository
{
    public class TempStaticRepository : IBookItRepository
    {

        public IEnumerable<Person> GetPersons()
        {
            return TempDb.GetPersons();
        }

        public IEnumerable<BookingSubject> GetAllBookingSubjects()
        {
            return TempDb.GetAllBookingSubjects();
        }

        public IEnumerable<BookingOffer> GetAllBookingOffers()
        {
            return TempDb.GetAllBookingOffers();
        }

        public void CreateBookingOffer(BookingOffer offer)
        {
            TempDb.SaveBookingOffer(offer);
        }

        public void UpdateBookingOffer(BookingOffer bookingOffer)
        {
            TempDb.UpdateBookingOffer(bookingOffer);
        }
    }
}
