using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using BookIt.BLL.Entities;
using TempDatabase;

namespace BookIt.Repository
{
    public class TempStaticRepository : IBookItRepository
    {

        public IEnumerable<UserDto> GetPersons()
        {
            return TempDb.GetPersons();
        }

        public IEnumerable<BookingSubjectDto> GetAllBookingSubjects()
        {
            return TempDb.GetAllBookingSubjects();
        }

        public IEnumerable<BookingOfferDto> GetAllBookingOffers()
        {
            return TempDb.GetAllBookingOffers();
        }

        public void CreateBookingOffer(BookingOfferDto offer)
        {
            TempDb.SaveBookingOffer(offer);
        }

        public void UpdateBookingOffer(BookingOfferDto bookingOffer)
        {
            TempDb.UpdateBookingOffer(bookingOffer);
        }

		#region IBookItRepository Members

		public IEnumerable<CategoryTypes> GetCategories()
		{
			throw new NotImplementedException();
		}

		#endregion


		public void CreateBookingSubject(BookingSubjectDto subject)
		{
			TempDb.SaveBookingSubject(subject);
		}
	}
}
