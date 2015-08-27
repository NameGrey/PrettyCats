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
        private readonly ISubjectsRepository _subjectsRepository;

		public SubjectsController(ISubjectsRepository repository)
		{
			_subjectsRepository = repository;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<Subject> GetAllSubjects()
		{
			return _subjectsRepository.Get();
		}

        [HttpGet]
        [Route("{id}")]
        public Subject GetSubjectById(int id)
        {
			return _subjectsRepository.GetByID(id);
        }

        [HttpGet]
		[Route("{subjectId:int}/offers")]
        public IEnumerable<Offer> GetOffersForSubject([FromUri]int subjectId)
        {
			var subject = _subjectsRepository.GetByID(subjectId);
			if (subject == null)
				return Enumerable.Empty<Offer>();

	        return subject.BookingOffers;
        }
    }
}
