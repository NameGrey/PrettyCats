using System.Linq;
using BookIt.DAL;
using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	public class BookingSubjectsMapper : IMapper<BLL.Entities.Subject, BookingSubject>
	{
		private readonly CategoriesMapper _categoriesMapper = new CategoriesMapper();
		private readonly UserMapper _userMapper = new UserMapper();
		private readonly BookingOffersMapper _bookingOffersMapper = new BookingOffersMapper();

		public BLL.Entities.Subject Map(BookingSubject source)
		{
			if (source == null)
				return null;

			BLL.Entities.Subject result = new BLL.Entities.Subject
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
				Capacity = source.Capacity,
				Category = _categoriesMapper.Map(source.Category),
				Owner = _userMapper.Map(source.Owner),
				BookingOffers = source.BookingOffers.Select(_bookingOffersMapper.Map).ToList()
			};
			return result;
		}

		public BookingSubject UnMap(BLL.Entities.Subject source)
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
	}
}
