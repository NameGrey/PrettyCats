using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using BookIt.BLL.Entities;
using BookIt.Repository;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
	    private readonly IOffersRepository _offersRepository;
	    private readonly IAccountService _accountService;


		public OffersController(IOffersRepository offersRepository, IAccountService accountService)
		{
		    _offersRepository = offersRepository;
		    _accountService = accountService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<Offer> GetAllBookingOffers()
		{
            return _offersRepository.Get();
		}

        [HttpGet]
        [ActionName("offers")]
        [Route("{id}")]
        public Offer GetOfferById(int id)
        {
            return _offersRepository.GetByID(id);
        }

        [HttpPost]
        [Route("{bookingOfferId:int}")]
        public IHttpActionResult BookOffer(TimeSlot bookingTimeSlot, [FromUri]int bookingOfferId)
        {
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

            Offer offer = _offersRepository.GetByID(bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

			if (offer.Book(bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, _accountService.GetCurrentUser()))
	        {
                _offersRepository.Update(offer);
                offer = _offersRepository.GetByID(bookingOfferId);
                return Ok(offer);
	        }
	        return InternalServerError();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult UnBookOffer(int slotId, int offerId)
        {
            Offer offer = _offersRepository.GetByID(offerId);
            if (offer == null) 
				return BadRequest("There are no data passed to unbook offer");

            if (offer.UnBook(slotId, _accountService.GetCurrentUser()))
            {
                _offersRepository.Update(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }
    }
}
