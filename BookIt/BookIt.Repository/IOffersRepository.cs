using System.Collections.Generic;
using BookIt.BLL.Entities;

namespace BookIt.Repository
{
    public interface IOffersRepository
    {
        void Update(Offer entityToUpdate);
        IEnumerable<Offer> Get();
        Offer GetByID(object id);
        void Insert(Offer entity);
        void Delete(object id);
        void Delete(Offer entityToDelete);
    }
}