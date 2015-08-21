using System.Collections.Generic;
using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class SubjectsService : ISubjectsService
	{
		private readonly IBookItRepository _repository;

		public SubjectsService(IBookItRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<BookingSubjectDto> GetAllSubjects()
		{
			return _repository.GetSubjects();
		}

		public BookingSubjectDto GetSubjectById(int subjectId)
		{
			return _repository.GetSubjects().FirstOrDefault(x=>x.Id == subjectId);
		}

	}
}
