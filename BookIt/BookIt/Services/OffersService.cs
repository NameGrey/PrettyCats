using System.Collections.Generic;
using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class OffersService : IOffersService
	{
		private readonly IBookItRepository _repository;

		public OffersService(IBookItRepository repository)
		{
			_repository = repository;
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
	}
}
