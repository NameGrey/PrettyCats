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
			IEnumerable<CategoryDto> categories = _categoriesService.GetAllCategories();

			var categoriesNames = new List<JObject>();
			foreach (CategoryDto category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = category.Id, category.Name }));
			}

			return categoriesNames;
		}
    }
}
