using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Subjects")]
    internal class SubjectsController : ApiController
    {
		private readonly ISubjectsService _subjectsService;
		private readonly IOffersService _offersService;

		public SubjectsController(ISubjectsService subjectsService, IOffersService offersService)
		{
			_subjectsService = subjectsService;
			_offersService = offersService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<BookingSubject> GetAllSubjects()
		{
			return _subjectsService.GetAllSubjects();
		}

        [HttpGet]
        [Route("{id}")]
        public BookingSubject GetSubjectById(int id)
        {
			return _subjectsService.GetSubjectById(id);
        }

        [HttpGet]
		[Route("{subjectId:int}/offers")]
        public IEnumerable<BookingOffer> GetOffersForSubject([FromUri]int subjectId)
        {
			return _offersService.GetAllOffersForSubject(subjectId);
        }
    }
}
