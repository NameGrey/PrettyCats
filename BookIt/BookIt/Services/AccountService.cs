using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class AccountService : IAccountService
	{
		private readonly IBookItRepository _repository;

		public AccountService(IBookItRepository repository)
		{
			_repository = repository;
		}

		public UserDto GetCurrentUser()
		{
			return _repository.GetUsers().FirstOrDefault(p => p.Id == 1);
		}

	}
}
