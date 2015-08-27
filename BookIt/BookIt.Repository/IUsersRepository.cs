using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.Repository
{
    public interface IUsersRepository
    {
        IEnumerable<User> Get();
        User GetByID(object id);
        void Insert(User entity);
        void Delete(object id);
        void Delete(User entityToDelete);
        void Update(User entityToUpdate);
    }
}