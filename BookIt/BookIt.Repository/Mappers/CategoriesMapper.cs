﻿using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public static class CategoriesMapper
	{
		public static Category UnMap(CategoryDto source)
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

		public static CategoryDto Map(Category source)
		{
			if (source == null)
				return null;

			CategoryDto result = new CategoryDto
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}

	}
}
