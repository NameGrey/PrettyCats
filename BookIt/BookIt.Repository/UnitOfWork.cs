using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;
using BookIt.DAL.Entities;

namespace BookIt.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context = new BookingContext();
        private GenericRepository<BookingOffer> _offersRepository;
        private GenericRepository<BookingSubject> _subjectRepository;
        private GenericRepository<User> _usersRepository;
        private GenericRepository<Role> _rolesRepository;
        private GenericRepository<Category> _categoriesRepository;
        private GenericRepository<TimeSlot> _timeSlotsRepository;


        private bool _disposed = false;

        public GenericRepository<TimeSlot> TimeSlotsRepository
        {
            get
            {
                return _timeSlotsRepository ?? (_timeSlotsRepository = new GenericRepository<TimeSlot>(_context));
            }
        }

        public GenericRepository<BookingOffer> OffersRepository
        {
            get
            {
                return _offersRepository ?? (_offersRepository = new GenericRepository<BookingOffer>(_context));
            }
        }

        public GenericRepository<BookingSubject> SubjectsRepository
        {
            get
            {
                return _subjectRepository ?? (_subjectRepository = new GenericRepository<BookingSubject>(_context));
            }
        }

        public GenericRepository<User> UsersRepository
        {
            get
            {
                return _usersRepository ?? (_usersRepository = new GenericRepository<User>(_context));
            }
        }

        public GenericRepository<Category> CategoriesRepository
        {
            get
            {
                return _categoriesRepository ?? (_categoriesRepository = new GenericRepository<Category>(_context));
            }
        }

        public GenericRepository<Role> RolesRepository
        {
            get
            {
                return _rolesRepository ?? (_rolesRepository = new GenericRepository<Role>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
