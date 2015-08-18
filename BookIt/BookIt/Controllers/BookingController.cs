using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookIt.BLL;

using BookIt.Repository;
using Newtonsoft.Json.Linq;


namespace BookIt.Controllers
{
	[RoutePrefix("api/Booking")]
	public class BookingController : ApiController
	{
		private readonly IBookItRepository _repository;

		public BookingController()
		{
			//repository = new TempStaticRepository();
			_repository = new DBRepository();
		}

		public BookingController(IBookItRepository repository)
		{
			_repository = repository;
		}


		private Person GetCurrentUser()
		{
#warning добавить авторизацию и получение пользователя
			//TODO должна быть логика по получению текущего пользователя
			//пока берем заранее подготовленного пользователя из временной базы            
			return _repository.GetPersons().FirstOrDefault(p => p.Id == 1);
		}

		[HttpGet]
		[ActionName("subjects")]
		public IEnumerable<BookingSubject> GetAllBookingSubjects()
		{
			return _repository.GetAllBookingSubjects();
		}

		[HttpGet]
		[ActionName("categories")]
		public IEnumerable<JObject> GetAllCategories()
		{
			IEnumerable<Category> categories = _repository.GetCategories();

#warning добавить локализацию
			var categoriesNames = new List<JObject>();
			foreach (Category category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = (int) category, Name = Enum.GetName(typeof(Category), category) }));
			}

			return categoriesNames;
		}

		[HttpGet]
		[ActionName("subjects")]
		public BookingSubject GetBookingSubject(int id)
		{
			return _repository.GetAllBookingSubjects().FirstOrDefault(s => s.Id == id);
		}

		[HttpGet]
		[ActionName("subjects")]
		[Route("subjects/{categoryId}/{text}")]
		public IEnumerable<BookingSubject> GetFilteredBookingSubject(Category category, string text)
		{
			return _repository.GetAllBookingSubjects().Where(s => s.Category == category && s.Name.ToUpper().Contains(text.ToUpper()));
		}

		[HttpGet]
		[ActionName("offers")]
		public IEnumerable<BookingOffer> GetAllBookingOffers()
		{
			return _repository.GetAllBookingOffers();
		}

		[HttpGet]
		[ActionName("offers")]
		[Route("offers/{id}")]
		public BookingOffer GetBookingOffer(int id)
		{
			return _repository.GetAllBookingOffers().FirstOrDefault(s => s.Id == id);
		}


		public IEnumerable<BookingOffer> GetFreeBookingOffers()
		{
			return _repository.GetAllBookingOffers().Where(o => o.IsOccupied == false);
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
			_repository.CreateBookingOffer(offer);
			return Ok(offer);
		}

		/// <summary>
		/// Добавляет новый предмет для резервирования. Функция доступна только пользователю с ролью Администратор.
		/// </summary>
		/// <param name="subject">Предмет для добавляения в словарю</param>
		/// <returns>Возвращает созданный в системе предмет для бронирования</returns>
		[HttpPost]
		[ActionName("subjects")]
		public IHttpActionResult CreateBookingSubject(BookingSubject subject)
		{
			if (subject == null) return BadRequest("There are no data passed to create subject");

#warning эта функция должна быть доступна только администратору, администратор должен иметь возможность указать owner-а
			subject.Owner = GetCurrentUser();
			_repository.CreateBookingSubject(subject);
			return Ok(subject);
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
			BookingSubject subject = _repository.GetAllBookingSubjects().FirstOrDefault(e => e.Id == bookingSubjectId);
			//TODO объект по идентификатору в справочнике не найден, надо бы вернуть ошибку
			if (subject == null) return NotFound();
			offer.FillFromSubject(subject);
			_repository.CreateBookingOffer(offer);
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
			BookingSubject subject = _repository.GetAllBookingSubjects().FirstOrDefault(e => e.Id == bookingSubjectId);
			//объект по идентификатору в справочнике не найден 
			if (subject == null) return null;
			return _repository.GetAllBookingOffers().Where(of => of.BookingSubjectId != null && of.BookingSubjectId.Value == bookingSubjectId);
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
		[ActionName("offers")]
		public IHttpActionResult CancelBook(int slotId, int offerId)
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
