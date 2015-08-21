using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.DAL;
using TempDatabase;

namespace BookIt.Repository
{
    public class TempStaticRepository : IBookItRepository
    {

        public IEnumerable<UserDto> GetUsers()
        {
            return TempDb.GetPersons();
        }

        public IEnumerable<BookingSubjectDto> GetSubjects()
        {
            return TempDb.GetAllBookingSubjects();
        }

        public IEnumerable<BookingOfferDto> GetOffers()
        {
            return TempDb.GetAllBookingOffers();
        }

        public void CreateOffer(BookingOfferDto offer)
        {
            TempDb.SaveBookingOffer(offer);
        }

        public void UpdateOffer(BookingOfferDto bookingOffer)
        {
            TempDb.UpdateBookingOffer(bookingOffer);
        }

		#region IBookItRepository Members

		public IEnumerable<CategoryDto> GetCategories()
		{
			throw new NotImplementedException();
		}

		#endregion


		public void CreateSubject(BookingSubjectDto subject)
		{
			TempDb.SaveBookingSubject(subject);
		}
	}
}
