using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	public class BookingOffersMapper : IMapper<BLL.Entities.Offer, BookingOffer>
	{
		private readonly CategoriesMapper _categoriesMapper  = new CategoriesMapper();
		private readonly TimeSlotsMapper _timeSlotsMapper = new TimeSlotsMapper();
		private readonly UserMapper _userMapper = new UserMapper();

		public BLL.Entities.Offer Map(BookingOffer source)
		{
			if (source == null)
				return null;

			BLL.Entities.Offer result = new BLL.Entities.Offer
			{
				Id = source.ID,
				BookingSubjectId = source.BookingSubjectID,
				Category = _categoriesMapper.Map(source.Category),
				Description = source.Description,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsInfinite = source.IsInfinite,
				Name = source.Name,
				Owner = _userMapper.Map(source.Owner),
			};

			var timeSlotsDto = source.TimeSlots;
			foreach (var timeSlot in timeSlotsDto)
			{
				result.TimeSlots.Add(_timeSlotsMapper.Map(timeSlot));
			}
			return result;
		}

		public BookingOffer UnMap(BLL.Entities.Offer source)
		{
			if (source == null)
				return null;

			BookingOffer result = new BookingOffer
			{
				ID = source.Id,
				BookingSubjectID = source.BookingSubjectId,
                CategoryID = source.Category.Id,
				Description = source.Description,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsInfinite = source.IsInfinite,
				Name = source.Name,
				OwnerID = source.Owner.Id
			};
			return result;
		}
	}
}
