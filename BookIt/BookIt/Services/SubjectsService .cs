using System.Collections.Generic;
using System.Linq;
using BookIt.BLL;
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

		public IEnumerable<BookingSubject> GetAllSubjects()
		{
			return _repository.GetAllBookingSubjects();
		}

		public BookingSubject GetSubjectById(int subjectId)
		{
			return _repository.GetAllBookingSubjects().FirstOrDefault(x=>x.Id == subjectId);
		}

	}
}
