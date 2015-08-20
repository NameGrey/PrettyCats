using BookIt.BLL;

namespace BookIt.Services
{
	internal interface IAccountService
	{
		Person GetCurrentUser();
	}
}