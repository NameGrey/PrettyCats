using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	public class TimeSlotsMapper : IMapper<BLL.Entities.TimeSlot, TimeSlot>
	{
		private readonly UserMapper _userMapper = new UserMapper();
		public TimeSlot UnMap(BLL.Entities.TimeSlot source)
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

		public BLL.Entities.TimeSlot Map(TimeSlot source)
		{
			if (source == null)
				return null;

			BLL.Entities.TimeSlot result = new BLL.Entities.TimeSlot
			{
				Id = source.ID,
				BookingOfferId = source.BookingOfferID,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				IsOccupied = source.IsOccupied,
				Owner = _userMapper.Map(source.Owner)
			};

			return result;
		}

	}
}
