
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.Controllers;
using BookIt.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace BookIt.Tests.Controllers
{
	[TestClass]
	public class CategoriesControllerTests
	{
		private MockRepository _mockRepository;
		private ICategoriesService _categoriesService;

		[TestInitialize]
		public void Setup()
		{
			_mockRepository = new MockRepository();
			_categoriesService = _mockRepository.DynamicMock<ICategoriesService>();
			_mockRepository.ReplayAll();
		}

		[TestMethod]
		public void GetAllCategoriesTest()
		{
			List<CategoryTypes> expectedResult = new List<CategoryTypes>(){CategoryTypes.Parking, CategoryTypes.Sport, CategoryTypes.Users};
			_categoriesService.Expect(x => x.GetAllCategories()).Return(expectedResult);
			_mockRepository.ReplayAll();
			var target = CreateTarget();
			var actual = target.GetAllCategories();
			Assert.IsNotNull(actual);
			var actualList = actual.ToList();

			Assert.AreEqual(expectedResult.Count, actualList.Count);
			for (int i = 0; i < expectedResult.Count; i++)
			{
				Assert.AreEqual(expectedResult[i].ToString(), actualList[i]["Name"].ToString());
			}
		}

		private CategoriesController CreateTarget()
		{
			return new CategoriesController(_categoriesService);
		}
	}
}
