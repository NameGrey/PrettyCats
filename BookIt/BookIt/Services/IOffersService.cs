using System.Collections.Generic;
using BookIt.BLL;

namespace BookIt.Services
{
	internal interface IOffersService
	{
		IEnumerable<BookingOffer> GetAllOffers();
		IEnumerable<BookingOffer> GetAllOffersForSubject(int subjectId);

		BookingOffer GetOfferById(int offerId);
		void UpdateOffer(BookingOffer offer);
	}
}