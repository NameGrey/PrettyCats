using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public static class BookingSubjectsMapper
	{
		public static BookingSubject UnMap(BookingSubjectDto source)
		{
			if (source == null)
				return null;

			BookingSubject result = new BookingSubject
			{
				ID = source.Id,
				Name = source.Name,
				Description = source.Description,
				Capacity = source.Capacity,
				CategoryID = source.Category.Id,
				OwnerID = source.Owner.Id,
			};
			return result;
		}

		public static BookingSubjectDto Map(BookingSubject source)
		{
			if (source == null)
				return null;

			BookingSubjectDto result = new BookingSubjectDto
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
				Capacity = source.Capacity,
				Category = CategoriesMapper.Map(source.Category),
				Owner = UserMapper.Map(source.Owner),
			};
			return result;
		}

		
	}
}
