using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.Repository
{
    public interface ISubjectsRepository
    {
        IEnumerable<Subject> Get();
        Subject GetByID(object id);
        void Insert(Subject entity);
        void Delete(object id);
        void Delete(Subject entityToDelete);
        void Update(Subject entityToUpdate);
    }
}