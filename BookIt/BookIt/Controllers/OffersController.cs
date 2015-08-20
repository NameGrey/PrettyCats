using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Repository;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
		private readonly IBookItRepository _repository;

        public OffersController(IBookItRepository repository)
		{
            _repository = repository;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<BookingOffer> GetAllBookingOffers()
		{
			return _repository.GetAllBookingOffers();
		}

        [HttpGet]
        [ActionName("offers")]
        [Route("{id}")]
        public BookingOffer GetOfferById(int id)
        {
            return _repository.GetAllBookingOffers().FirstOrDefault(s => s.Id == id);
        }

        [HttpPost]
        [Route("{bookingOfferId:int}")]
        public IHttpActionResult BookOffer(BookingTimeSlot bookingTimeSlot, [FromUri]int bookingOfferId)
        {
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

            BookingOffer offer = _repository.GetAllBookingOffers().FirstOrDefault(o => o.Id == bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

            if (offer.Book(bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, GetCurrentUser()))
            {
                _repository.UpdateBookingOffer(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult UnBookOffer(int slotId, int offerId)
        {
            BookingOffer offer = _repository.GetAllBookingOffers().FirstOrDefault(o => o.Id == offerId);
            if (offer == null) return BadRequest("There are no data passed to unbook offer");
            if (offer.UnBook(slotId, GetCurrentUser()))
            {
                _repository.UpdateBookingOffer(offer);
                return Ok(offer);
            }
            return InternalServerError();
        }


        private Person GetCurrentUser()
        {
#warning добавить авторизацию и получение пользователя
            //TODO должна быть логика по получению текущего пользователя
            //пока берем заранее подготовленного пользователя из временной базы            
            return _repository.GetPersons().FirstOrDefault(p => p.Id == 1);
        }
    }
}
