using System;
using System.Collections.Generic;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.Services;
using Newtonsoft.Json.Linq;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
		private readonly ICategoriesService _categoriesService;

		public CategoriesController(ICategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<JObject> GetAllCategories()
		{
			IEnumerable<CategoryTypes> categories = _categoriesService.GetAllCategories();

#warning добавить локализацию
			var categoriesNames = new List<JObject>();
			foreach (CategoryTypes category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = (int)category, Name = Enum.GetName(typeof(CategoryTypes), category) }));
			}

			return categoriesNames;
		}
    }
}
