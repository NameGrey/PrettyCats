using System.Collections.Generic;
using System.Linq;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class CategoriesRepository : IGenericRepository<BLL.Entities.Category>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly CategoriesMapper _mapper = new CategoriesMapper();

		private GenericRepository<DAL.Entities.Category> Repository
		{
			get { return _unitOfWork.CategoriesRepository; }
		}

		public CategoriesRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<BLL.Entities.Category> Get()
		{
			return Repository.Get().Select(_mapper.Map).ToList();
		}

		public BLL.Entities.Category GetByID(object id)
		{
			return _mapper.Map(Repository.GetByID(id));
		}

		public void Insert(BLL.Entities.Category entity)
		{
			Repository.Insert(_mapper.UnMap(entity));
		}

		public void Delete(object id)
		{
			Repository.Delete(id);
		}

		public void Delete(BLL.Entities.Category entityToDelete)
		{
			Repository.Delete(_mapper.UnMap(entityToDelete));
		}

		public void Update(BLL.Entities.Category entityToUpdate)
		{
			Repository.Update(_mapper.UnMap(entityToUpdate));
		}
	}
}
