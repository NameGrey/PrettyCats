using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class SubjectsRepository : GenericRepository<BLL.Entities.Subject, DAL.Entities.BookingSubject>, ISubjectsRepository
	{
	    public SubjectsRepository() : base(new BookingContext(), new BookingSubjectsMapper())
	    {
	    }
	}
}
