using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Repository;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
		private readonly IGenericRepository<Offer> _repository;
		private readonly IAccountService _accountService;


		public OffersController(IGenericRepository<Offer> repository, IAccountService accountService)
		{
			_repository = repository;
			_accountService = accountService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<Offer> GetAllBookingOffers()
		{
			return _repository.Get();
		}

        [HttpGet]
        [ActionName("offers")]
        [Route("{id}")]
        public Offer GetOfferById(int id)
        {
			return _repository.GetByID(id);
        }

        [HttpPost]
        [Route("{bookingOfferId:int}")]
        public IHttpActionResult BookOffer(TimeSlot bookingTimeSlot, [FromUri]int bookingOfferId)
        {
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

			Offer offer = _repository.GetByID(bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

			if (offer.Book(bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, _accountService.GetCurrentUser()))
	        {
				_repository.Update(offer);
		        return Ok(offer);
	        }
	        return InternalServerError();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult UnBookOffer(int slotId, int offerId)
        {
			Offer offer = _repository.GetByID(offerId);
            if (offer == null) 
				return BadRequest("There are no data passed to unbook offer");

			if (offer.UnBook(slotId, _accountService.GetCurrentUser()))
				return Ok(offer);
			return InternalServerError();
        }
    }
}
