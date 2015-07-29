using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookIt.BLL;

using BookIt.Repository;


namespace BookIt.Controllers
{
	[RoutePrefix("api/Booking")]
	public class BookingController : ApiController
	{
		IBookItRepository repository = null;

		public BookingController()
		{
			repository = new TempStaticRepository();
		}

		public BookingController(IBookItRepository repository)
		{
			this.repository = repository;
		}


		private Person GetCurrentUser()
		{
			//TODO должна быть логика по получению текущего пользователя
			//пока берем заранее подготовленного пользователя из временной базы            
			return repository.GetPersons().FirstOrDefault(p => p.Id == 3);
		}

		[HttpGet]
		[ActionName("subjects")]
		public IEnumerable<BookingSubject> GetAllBookingSubjects()
		{
			return repository.GetAllBookingSubjects();
		}

        [HttpGet]
        [ActionName("subjects")]
        public BookingSubject GetBookingSubject(int id)
        {
            return repository.GetAllBookingSubjects().FirstOrDefault(s => s.Id == id); 
        }

        [HttpGet]
        [ActionName("subjects")]
        [Route("subjects/{categoryId}/{text}")]
        public IEnumerable<BookingSubject> GetFilteredBookingSubject(int categoryId, string text)
        {
            return repository.GetAllBookingSubjects().Where(s => s.CategoryId == categoryId && s.Name.ToUpper().Contains(text.ToUpper()));
        }

        [HttpGet]
        [ActionName("offers")]
        public IEnumerable<BookingOffer> GetAllBookingOffers()
        {
            return repository.GetAllBookingOffers();
        }

        [HttpGet]
        [ActionName("offers")]
		[Route("offers/{id}")]
        public BookingOffer GetBookingOffer(int id)
        {
            return repository.GetAllBookingOffers().FirstOrDefault(s => s.Id == id);
        }


        public IEnumerable<BookingOffer> GetFreeBookingOffers()
        {
            return repository.GetAllBookingOffers().Where(o => o.IsOccupied == false);
        }


		/// <summary>
		/// Создает предложение для резервирования на период для любого объекта без привязки к справочнику
		/// </summary>
		/// <param name="subjectName">Наименоывание предмета для букинга</param>
		/// <param name="startDate">Дата начала предложения</param>
		/// <param name="endDate">Дата окончания предложения</param>
		/// <returns>Возвращает созданное в системе предложение для резервирования </returns>
		[HttpPost]
		[ActionName("offers")]
		public IHttpActionResult CreateBookingOffer(BookingOffer offer)
		{
			if (offer == null) return BadRequest("There are no data passed to create offer");

			offer.Owner = GetCurrentUser();
			offer.FillCustomBookingOffer();
			repository.SaveBookingOffer(offer);
			return Ok(offer);
		}


		/// <summary>
		/// Создает предложение для резервирования выбранного из справочника объекта
		/// </summary>
		/// <param name="bookingSubjectId">Идентификатор резервируемого объекта из справочника</param>
		/// <param name="startDate">Дата начала предложения</param>
		/// <param name="endDate">Дата окончания предложения</param>
		/// <returns>Возвращает созданное в системе предложение для резервирования </returns>
		[HttpPost]
		[Route("subjects/{bookingSubjectId:int}/offers")]
		public IHttpActionResult CreateBookingOfferForSubject(BookingOffer offer, [FromUri]int bookingSubjectId)
		{
			BookingSubject subject = repository.GetAllBookingSubjects().FirstOrDefault(e => e.Id == bookingSubjectId);
			//TODO объект по идентификатору в справочнике не найден, надо бы вернуть ошибку
			if (subject == null) return NotFound(); 
			offer.FillFromSubject(subject);
			repository.SaveBookingOffer(offer);
			return Ok(offer); 
		}

		/// <summary>
		/// Список предложений по выбранному объекту
		/// </summary>
		/// <param name="bookingSubjectId">Идентификатор объекта</param>
		/// <returns></returns>
		[HttpGet]
		[Route("subjects/{bookingSubjectId:int}/offers")]
		public IEnumerable<BookingOffer> GetBookingOfferForSubject([FromUri]int bookingSubjectId)
		{
			BookingSubject subject = repository.GetAllBookingSubjects().FirstOrDefault(e => e.Id == bookingSubjectId);
			//объект по идентификатору в справочнике не найден 
			if (subject == null) return null;		
			return repository.GetAllBookingOffers().Where(of => of.BookingSubjectId != null && of.BookingSubjectId.Value == bookingSubjectId);
		}

		/// <summary>
		/// Бронировать конкретное предложение на указанный период
		/// </summary>
		/// <param name="id">Идентификатор выбранного предложения для резервирования</param>
		/// <param name="startDate">Дата начала резервирования</param>
		/// <param name="endDate">Дата окончания резервирования</param>
		/// <returns>Признак, успешно забронировано или нет</returns>
		[HttpPost]
		[Route("offers/{bookingOfferId:int}")]
		[ActionName("offers")]
        public IHttpActionResult BookIt(BookingTimeSlot bookingTimeSlot, [FromUri]int bookingOfferId)
		{
            if (bookingTimeSlot == null) return BadRequest("There are no data passed to book offer");

			BookingOffer offer = repository.GetAllBookingOffers().FirstOrDefault(o => o.Id == bookingOfferId);

            if (offer == null) return BadRequest("There are no data passed to book offer");

			if (offer.Book(bookingTimeSlot.StartDate, bookingTimeSlot.EndDate, GetCurrentUser()))
			{
				repository.UpdateBookingOffer(offer);
                return Ok(offer);
			}
            return InternalServerError();
		}

		[HttpDelete]
		[ActionName("offers")]
		public bool CancelBook(int bookingOfferId, int timeSlotID)
		{
			BookingOffer offer = repository.GetAllBookingOffers().FirstOrDefault(o => o.Id == bookingOfferId);
			if (offer == null) return false;

			return offer.UnBook(timeSlotID, GetCurrentUser());
		}

		/// <summary>
		/// Handle pre-flight OPTION request, that is making by cllents during CORS requests
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage Options()
		{
			var response = new HttpResponseMessage();
			response.StatusCode = HttpStatusCode.OK;
			return response;
		}
	}
}
