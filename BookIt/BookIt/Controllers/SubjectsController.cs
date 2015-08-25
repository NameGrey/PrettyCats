using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Repository;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Subjects")]
    public class SubjectsController : ApiController
    {
		private readonly IGenericRepository<Subject> _repository;

		public SubjectsController(IGenericRepository<Subject> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<Subject> GetAllSubjects()
		{
			return _repository.Get();
		}

        [HttpGet]
        [Route("{id}")]
        public Subject GetSubjectById(int id)
        {
			return _repository.GetByID(id);
        }

        [HttpGet]
		[Route("{subjectId:int}/offers")]
        public IEnumerable<Offer> GetOffersForSubject([FromUri]int subjectId)
        {
			var subject = _repository.GetByID(subjectId);
			if (subject == null)
				return Enumerable.Empty<Offer>();

	        return subject.BookingOffers;
        }
    }
}
