using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.BLL.Services;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class OffersService : IOffersService
	{
		private readonly IBookItRepository _repository;
		private readonly IBookingService _bookingService;
		private readonly IAccountService _accountService;

		public OffersService(IBookItRepository repository, IBookingService bookingService, IAccountService accountService)
		{
			_repository = repository;
			_bookingService = bookingService;
			_accountService = accountService;
		}

		public IEnumerable<BookingOfferDto> GetAllOffers()
		{
			return _repository.GetAllBookingOffers();
		}

		public IEnumerable<BookingOfferDto> GetAllOffersForSubject(int subjectId)
		{
			return _repository.GetAllBookingOffers().Where(x => x.BookingSubjectId.HasValue && x.BookingSubjectId.Value == subjectId).ToList();
		}

		public BookingOfferDto GetOfferById(int offerId)
		{
			return _repository.GetAllBookingOffers().FirstOrDefault(x=>x.Id == offerId);
		}

		public void UpdateOffer(BookingOfferDto offer)
		{
			_repository.UpdateBookingOffer(offer);
		}

		public bool BookOffer(BookingOfferDto offer, DateTime startDate, DateTime endDate)
		{
			if (_bookingService.Book(offer, startDate, endDate, _accountService.GetCurrentUser()))
			{
				UpdateOffer(offer);
				return true;
			}
			return false;
		}

		public bool UnBookOffer(BookingOfferDto offer, int timeSlotId)
		{
			if (_bookingService.UnBook(offer, timeSlotId, _accountService.GetCurrentUser()))
			{
				UpdateOffer(offer);
				return true;
			}
			return false;
		}
	}
}
