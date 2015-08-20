using System.Collections.Generic;
using BookIt.BLL;
using BookIt.BLL.Entities;
using BookIt.Repository;

namespace BookIt.Services
{
	internal class CategoriesService : ICategoriesService
	{
		private readonly IBookItRepository _repository;

		public CategoriesService(IBookItRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<CategoryTypes> GetAllCategories()
		{
			return _repository.GetCategories();
		}

	}
}
