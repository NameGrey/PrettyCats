using System;
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
		bool BookOffer(BookingOfferDto offer, DateTime startDate, DateTime endDate);
		bool UnBookOffer(BookingOfferDto offer, int timeSlotId);

	}
}