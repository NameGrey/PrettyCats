using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class SubjectsRepository : IGenericRepository<Subject>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly BookingSubjectsMapper _bookingSubjectsMapper = new BookingSubjectsMapper();

		private GenericRepository<BookingSubject> Repository
		{
			get { return _unitOfWork.SubjectsRepository; }
		}

		public SubjectsRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Subject> Get()
		{
			return Repository.Get().Select(_bookingSubjectsMapper.Map).ToList();
		}

		public Subject GetByID(object id)
		{
			return _bookingSubjectsMapper.Map(Repository.GetByID(id));
		}

		public void Insert(Subject entity)
		{
			Repository.Insert(_bookingSubjectsMapper.UnMap(entity));
		}

		public void Delete(object id)
		{
			Repository.Delete(id);
		}

		public void Delete(Subject entityToDelete)
		{
			Repository.Delete(_bookingSubjectsMapper.UnMap(entityToDelete));
		}

		public void Update(Subject entityToUpdate)
		{
			Repository.Update(_bookingSubjectsMapper.UnMap(entityToUpdate));
		}
	}
}
