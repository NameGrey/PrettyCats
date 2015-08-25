using System;
using System.Collections.Generic;
using BookIt.BLL.Entities;
using TempDatabase;

namespace BookIt.Repository
{
    public class TempStaticRepository 
    {

		public IEnumerable<BLL.Entities.User> GetUsers()
        {
            return TempDb.GetPersons();
        }

		public IEnumerable<BLL.Entities.Subject> GetSubjects()
        {
            return TempDb.GetAllBookingSubjects();
        }

		public IEnumerable<BLL.Entities.Offer> GetOffers()
        {
            return TempDb.GetAllBookingOffers();
        }

		public void CreateOffer(BLL.Entities.Offer offer)
        {
            TempDb.SaveBookingOffer(offer);
        }

		public void UpdateOffer(BLL.Entities.Offer bookingOffer)
        {
            TempDb.UpdateBookingOffer(bookingOffer);
        }

		#region IBookItRepository Members

		public IEnumerable<BLL.Entities.Category> GetCategories()
		{
			throw new NotImplementedException();
		}

		#endregion


		public void CreateSubject(Subject subject)
		{
			TempDb.SaveBookingSubject(subject);
		}
	}
}
