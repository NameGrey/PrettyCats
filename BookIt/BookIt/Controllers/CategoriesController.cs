using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Repository;
using Newtonsoft.Json.Linq;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
		private readonly IGenericRepository<Category> _repository;

		public CategoriesController(IGenericRepository<Category> repository)
		{
			_repository = repository;

		}

		[HttpGet]
		[Route("")]
		public IEnumerable<JObject> GetAllCategories()
		{
			IEnumerable<Category> categories = _repository.Get();

			var categoriesNames = new List<JObject>();
			foreach (Category category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = category.Id, category.Name }));
			}

			return categoriesNames;
		}
    }
}
