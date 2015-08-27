using System.Linq;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class AccountService : IAccountService
	{
        private readonly IUsersRepository _usersRepository;

        public AccountService(IUsersRepository usersRepository)
		{
            _usersRepository = usersRepository;
		}

		public User GetCurrentUser()
		{
            return _usersRepository.GetByID(1);
		}

	}
}
