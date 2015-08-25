using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	public class CategoriesMapper : IMapper<BLL.Entities.Category, Category>
	{
		public Category UnMap(BLL.Entities.Category source)
		{
			if (source == null)
				return null;

			Category result = new Category
			{
				ID = source.Id,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}

		public BLL.Entities.Category Map(Category source)
		{
			if (source == null)
				return null;

			BLL.Entities.Category result = new BLL.Entities.Category
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}

	}
}
