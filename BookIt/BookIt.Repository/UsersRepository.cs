using System.Collections.Generic;
using System.Linq;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class UsersRepository : IGenericRepository<BLL.Entities.User>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserMapper _userMapper = new UserMapper();

		private GenericRepository<DAL.Entities.User> Repository
		{
			get { return _unitOfWork.UsersRepository; }
		}

		public UsersRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<BLL.Entities.User> Get()
		{
			return Repository.Get().Select(_userMapper.Map).ToList();
		}

		public BLL.Entities.User GetByID(object id)
		{
			return _userMapper.Map(Repository.GetByID(id));
		}

		public void Insert(BLL.Entities.User entity)
		{
			Repository.Insert(_userMapper.UnMap(entity));
		}

		public void Delete(object id)
		{
			Repository.Delete(id);
		}

		public void Delete(BLL.Entities.User entityToDelete)
		{
			Repository.Delete(_userMapper.UnMap(entityToDelete));
		}

		public void Update(BLL.Entities.User entityToUpdate)
		{
			Repository.Update(_userMapper.UnMap(entityToUpdate));
		}
	}
}
