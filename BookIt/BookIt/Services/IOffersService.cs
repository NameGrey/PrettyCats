using System.Collections.Generic;
using BookIt.BLL;
using BookIt.BLL.Entities;

namespace BookIt.Services
{
    public interface IOffersService
	{
		IEnumerable<BookingOfferDto> GetAllOffers();
		IEnumerable<BookingOfferDto> GetAllOffersForSubject(int subjectId);

		BookingOfferDto GetOfferById(int offerId);
		void UpdateOffer(BookingOfferDto offer);
	}
}