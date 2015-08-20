using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Repository;
using BookIt.Services;
using Newtonsoft.Json.Linq;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Categories")]
    internal class CategoriesController : ApiController
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
			IEnumerable<Category> categories = _categoriesService.GetAllCategories();

#warning добавить локализацию
			var categoriesNames = new List<JObject>();
			foreach (Category category in categories)
			{
				categoriesNames.Add(JObject.FromObject(new { CategoryId = (int)category, Name = Enum.GetName(typeof(Category), category) }));
			}

			return categoriesNames;
		}
    }
}
