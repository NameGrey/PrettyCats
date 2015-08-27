using BookIt.DAL;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
    public class UsersRepository : GenericRepository<BLL.Entities.User, DAL.Entities.User>, IUsersRepository
    {
        public UsersRepository() : base(new BookingContext(), new UserMapper())
        {
        }
	}
}
