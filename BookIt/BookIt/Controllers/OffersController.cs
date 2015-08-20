using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Repository;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    internal class OffersController : ApiController
    {
		private readonly IOffersService _offersService;
		private readonly IAccountService _accountService;

		public OffersController(IOffersService offersService, IAccountService accountService)
		{
			_offersService = offersService;
			_accountService = accountService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<BookingOffer> GetAllBookingOffers()
		{
			return _offersService.GetAllOffers();
		}

        [HttpGet]
        [ActionName("offers")]
        [Route("{id}")]
        public BookingOffer GetOfferById(int id)
        {
			return _offersService.GetOfferById(id);
        }

        [HttpPost]
        [Route("{bookingOfferId:int}")]
        public IHttpActionResult BookOffer(BookingTimeSlot bookingTimeSlot, [FromUri]int bookingOfferId)
        {
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

			BookingOffer offer = _offersService.GetOfferById(bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

			if (offer.Book(bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, CurrentUser))
            {
                _offersService.UpdateOffer(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult UnBookOffer(int slotId, int offerId)
        {
			BookingOffer offer = _offersService.GetOfferById(offerId);
            if (offer == null) 
				return BadRequest("There are no data passed to unbook offer");
			if (offer.UnBook(slotId, CurrentUser))
            {
				_offersService.UpdateOffer(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }


		private Person CurrentUser
		{
			get { return _accountService.GetCurrentUser(); }
		}
    }
}
