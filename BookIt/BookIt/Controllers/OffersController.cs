using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.BLL.Services;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
		private readonly IOffersService _offersService;
		private readonly IAccountService _accountService;
	    private readonly IBookingService _bookingService;

	    public OffersController(IOffersService offersService, IAccountService accountService, IBookingService bookingService)
		{
			_offersService = offersService;
			_accountService = accountService;
	        _bookingService = bookingService;
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

			if (_bookingService.Book(offer, bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, CurrentUser))
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
			BookingOfferDto offer = _offersService.GetOfferById(offerId);
            if (offer == null) 
				return BadRequest("There are no data passed to unbook offer");
            if (_bookingService.UnBook(offer,slotId, CurrentUser))
            {
				_offersService.UpdateOffer(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }


		private UserDto CurrentUser
		{
			get { return _accountService.GetCurrentUser(); }
		}
    }
}
