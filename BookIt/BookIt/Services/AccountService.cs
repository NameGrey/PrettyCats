using System.Linq;
using BookIt.BLL;
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

		public Person GetCurrentUser()
		{
			return _repository.GetPersons().FirstOrDefault(p => p.Id == 1);
		}

	}
}
