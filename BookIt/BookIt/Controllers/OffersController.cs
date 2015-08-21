using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
		private readonly IOffersService _offersService;


	    public OffersController(IOffersService offersService)
		{
			_offersService = offersService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<BookingOfferDto> GetAllBookingOffers()
		{
			return _offersService.GetAllOffers();
		}

        [HttpGet]
        [ActionName("offers")]
        [Route("{id}")]
        public BookingOfferDto GetOfferById(int id)
        {
			return _offersService.GetOfferById(id);
        }

        [HttpPost]
        [Route("{bookingOfferId:int}")]
        public IHttpActionResult BookOffer(BookingTimeSlotDto bookingTimeSlot, [FromUri]int bookingOfferId)
        {
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

			BookingOfferDto offer = _offersService.GetOfferById(bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

			if (_offersService.BookOffer(offer, bookingTimeSlot.StartDate, bookingTimeSlot.EndDate))
				return Ok(offer);
            return InternalServerError();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult UnBookOffer(int slotId, int offerId)
        {
			BookingOfferDto offer = _offersService.GetOfferById(offerId);
            if (offer == null) 
				return BadRequest("There are no data passed to unbook offer");

			if (_offersService.UnBookOffer(offer, slotId))
				return Ok(offer);
			return InternalServerError();
        }
    }
}
