using System.Collections.Generic;
using System.Linq;
using BookIt.BLL;
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

		public IEnumerable<BookingOffer> GetAllOffers()
		{
			return _repository.GetAllBookingOffers();
		}

		public IEnumerable<BookingOffer> GetAllOffersForSubject(int subjectId)
		{
			return _repository.GetAllBookingOffers().Where(x => x.BookingSubjectId.HasValue && x.BookingSubjectId.Value == subjectId).ToList();
		}

		public BookingOffer GetOfferById(int offerId)
		{
			return _repository.GetAllBookingOffers().FirstOrDefault(x=>x.Id == offerId);
		}

		public void UpdateOffer(BookingOffer offer)
		{
			_repository.UpdateBookingOffer(offer);
		}
	}
}
