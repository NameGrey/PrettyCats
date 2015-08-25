using System;
using BookIt.DAL.Entities;

namespace BookIt.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<BookingOffer> OffersRepository { get; }
        GenericRepository<BookingSubject> SubjectsRepository { get; }
        GenericRepository<User> UsersRepository { get; }
        GenericRepository<Category> CategoriesRepository { get; }
        GenericRepository<Role> RolesRepository { get; }
        void Save();
    }
}