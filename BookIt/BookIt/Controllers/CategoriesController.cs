using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookIt.BLL;
using BookIt.Repository;
using Newtonsoft.Json.Linq;

namespace BookIt.Controllers
{
	[RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly IBookItRepository _repository;

        public CategoriesController(IBookItRepository repository)
		{
            _repository = repository;
		}

		[HttpGet]
		[Route("")]
		public IEnumerable<JObject> GetAllCategories()
		{
			IEnumerable<Category> categories = _repository.GetCategories();

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
