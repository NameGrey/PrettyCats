using BookIt.BLL;
using BookIt.BLL.Entities;

namespace BookIt.Services
{
    public interface IAccountService
	{
		User GetCurrentUser();
	}
}