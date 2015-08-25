using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class AccountService : IAccountService
	{
		private readonly IGenericRepository<User> _repository;

		public AccountService(IGenericRepository<User> repository)
		{
			_repository = repository;
		}

		public User GetCurrentUser()
		{
			return _repository.GetByID(1);
		}

	}
}
