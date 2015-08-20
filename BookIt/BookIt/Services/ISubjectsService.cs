using System.Collections.Generic;
using BookIt.BLL;

namespace BookIt.Services
{
	internal interface ISubjectsService
	{
		IEnumerable<BookingSubject> GetAllSubjects();
		BookingSubject GetSubjectById(int subjectId);
	}
}