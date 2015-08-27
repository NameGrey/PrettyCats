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
	    private readonly ICategoriesRepository _categoriesRepository;

		public CategoriesController(ICategoriesRepository categoriesRepository)
		{
		    _categoriesRepository = categoriesRepository;
		}

	    [HttpGet]
		[Route("")]
		public IEnumerable<JObject> GetAllCategories()
		{
            IEnumerable<Category> categories = _categoriesRepository.Get();

			var categoriesNames = new List<JObject>();
			foreach (Category category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = category.Id, category.Name }));
			}

			return categoriesNames;
		}
    }
}
