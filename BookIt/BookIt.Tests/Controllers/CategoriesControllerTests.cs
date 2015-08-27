using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.Controllers;
using BookIt.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace BookIt.Tests.Controllers
{
	[TestClass]
	public class CategoriesControllerTests
	{
		private MockRepository _mockRepository;
        private ICategoriesRepository _repository;

		[TestInitialize]
		public void Setup()
		{
			_mockRepository = new MockRepository();
			_repository = _mockRepository.DynamicMock<ICategoriesRepository>();
			_mockRepository.ReplayAll();
		}

		[TestMethod]
		public void GetAllCategoriesTest()
		{
			List<Category> expectedResult = new List<Category>();
			expectedResult.Add(new Category() {Id = 1, Name = "Sport"});
			expectedResult.Add(new Category() { Id = 2, Name = "Parking" });
			expectedResult.Add(new Category() { Id = 3, Name = "User" });


			_repository.Expect(x => x.Get()).Return(expectedResult);
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
			return new CategoriesController(_repository);
		}
	}
}
