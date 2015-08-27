using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
    public class CategoriesRepository : GenericRepository<BLL.Entities.Category, DAL.Entities.Category>, ICategoriesRepository
    {
        public CategoriesRepository() : base(new BookingContext(), new CategoriesMapper())
        {
        }
	}
}
