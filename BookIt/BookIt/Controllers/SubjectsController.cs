using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Services;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Subjects")]
    public class SubjectsController : ApiController
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
		public IEnumerable<BookingSubjectDto> GetAllSubjects()
		{
			return _subjectsService.GetAllSubjects();
		}

        [HttpGet]
        [Route("{id}")]
        public BookingSubjectDto GetSubjectById(int id)
        {
			return _subjectsService.GetSubjectById(id);
        }

        [HttpGet]
		[Route("{subjectId:int}/offers")]
        public IEnumerable<BookingOfferDto> GetOffersForSubject([FromUri]int subjectId)
        {
			return _offersService.GetAllOffersForSubject(subjectId);
        }
    }
}
