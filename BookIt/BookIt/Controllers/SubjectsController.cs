using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Repository;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Subjects")]
    public class SubjectsController : ApiController
    {
        private readonly IBookItRepository _repository;

        public SubjectsController(IBookItRepository repository)
		{
            _repository = repository;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<BookingSubject> GetAllSubjects()
		{
			return _repository.GetAllBookingSubjects();
		}

        [HttpGet]
        [Route("{id}")]
        public BookingSubject GetSubjectById(int id)
        {
            return _repository.GetAllBookingSubjects().FirstOrDefault(s => s.Id == id);
        }

        [HttpGet]
        [Route("{bookingSubjectId:int}/offers")]
        public IEnumerable<BookingOffer> GetOffersForSubject([FromUri]int bookingSubjectId)
        {
            BookingSubject subject = _repository.GetAllBookingSubjects().FirstOrDefault(e => e.Id == bookingSubjectId);
            if (subject == null) return null;
            return _repository.GetAllBookingOffers().Where(of => of.BookingSubjectId != null && of.BookingSubjectId.Value == bookingSubjectId);
        }
    }
}
