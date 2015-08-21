using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public static class TimeSlotsMapper
	{
		public static TimeSlot UnMap(BookingTimeSlotDto source)
		{
			if (source == null)
				return null;

			TimeSlot result = new TimeSlot
			{
				ID = source.Id,
				BookingOfferID = source.BookingOfferId,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsOccupied = source.IsOccupied,
				OwnerID = source.Owner != null ? source.Owner.Id: (int?)null
			};

			return result;
		}

		public static BookingTimeSlotDto Map(TimeSlot source)
		{
			if (source == null)
				return null;

			BookingTimeSlotDto result = new BookingTimeSlotDto
			{
				Id = source.ID,
				BookingOfferId = source.BookingOfferID,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsOccupied = source.IsOccupied,
				Owner = UserMapper.Map(source.Owner)
			};

			return result;
		}

	}
}
