using System.Collections.Generic;
using BookIt.BLL;

namespace BookIt.Services
{
	internal interface ICategoriesService
	{
		IEnumerable<Category> GetAllCategories();
	}
}