
using System;
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
			List<CategoryDto> expectedResult = new List<CategoryDto>();
			expectedResult.Add(new CategoryDto() {Id = 1, Name = "Sport"});
			expectedResult.Add(new CategoryDto() { Id = 2, Name = "Parking" });
			expectedResult.Add(new CategoryDto() { Id = 3, Name = "User" });


			_categoriesService.Expect(x => x.GetAllCategories()).Return(expectedResult);
			_mockRepository.ReplayAll();
			var target = CreateTarget();
			var actual = target.GetAllCategories();
			Assert.IsNotNull(actual);
			var actualList = actual.ToList();

			Assert.AreEqual(expectedResult.Count, actualList.Count);
			for (int i = 0; i < expectedResult.Count; i++)
			{
				Assert.AreEqual(expectedResult[i].Name, actualList[i]["Name"].ToString());
			}
		}

		private CategoriesController CreateTarget()
		{
			return new CategoriesController(_categoriesService);
		}
	}
}
