using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.Repository
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Get();
        Category GetByID(object id);
        void Insert(Category entity);
        void Delete(object id);
        void Delete(Category entityToDelete);
        void Update(Category entityToUpdate);
    }
}