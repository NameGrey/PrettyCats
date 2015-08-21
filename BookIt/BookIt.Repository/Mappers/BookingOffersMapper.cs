using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public static class BookingOffersMapper
	{
		public static BookingOffer UnMap(BookingOfferDto source)
		{
			if (source == null)
				return null;

			BookingOffer result = new BookingOffer
			{
				ID = source.Id,
				BookingSubjectID = source.BookingSubjectId,
				Category = CategoriesMapper.UnMap(source.Category),
				Description = source.Description,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsInfinite = source.IsInfinite,
				Name = source.Name,
				OwnerID = source.Owner.Id
			};
			return result;
		}

		public static BookingOfferDto Map(BookingOffer source)
		{
			if (source == null)
				return null;

			BookingOfferDto result = new BookingOfferDto
			{
				Id = source.ID,
				BookingSubjectId = source.BookingSubjectID,
				Category = CategoriesMapper.Map(source.Category),
				Description = source.Description,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsInfinite = source.IsInfinite,
				Name = source.Name,
				Owner = UserMapper.Map(source.Owner),
			};

			var timeSlotsDto = source.TimeSlots;
			foreach (var timeSlot in timeSlotsDto)
			{
				result.TimeSlots.Add(TimeSlotsMapper.Map(timeSlot));
			}
			return result;
		}
	}
}
