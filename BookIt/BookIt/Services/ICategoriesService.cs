using System.Collections.Generic;
using BookIt.BLL;
using BookIt.BLL.Entities;

namespace BookIt.Services
{
    public interface ICategoriesService
	{
		IEnumerable<CategoryTypes> GetAllCategories();
	}
}