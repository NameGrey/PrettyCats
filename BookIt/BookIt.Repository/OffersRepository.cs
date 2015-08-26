using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class OffersRepository : IGenericRepository<Offer>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly BookingOffersMapper _bookingOffersMapper = new BookingOffersMapper();
        private readonly TimeSlotsMapper _timeSlotsMapper = new TimeSlotsMapper();


		private GenericRepository<BookingOffer> Offers
		{
			get { return _unitOfWork.OffersRepository; }
		}

        private GenericRepository<DAL.Entities.TimeSlot> TimeSlots
        {
            get { return _unitOfWork.TimeSlotsRepository; }
        }

		public OffersRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Offer> Get()
		{
			return Offers.Get().Select(_bookingOffersMapper.Map).ToList();
		}

		public Offer GetByID(object id)
		{
			return _bookingOffersMapper.Map(Offers.GetByID(id));
		}

		public void Insert(Offer entity)
		{
			Offers.Insert(_bookingOffersMapper.UnMap(entity));
		}

		public void Delete(object id)
		{
			Offers.Delete(id);
		}

		public void Delete(Offer entityToDelete)
		{
			Offers.Delete(_bookingOffersMapper.UnMap(entityToDelete));
		}

		public void Update(Offer entityToUpdate)
		{
		    var existingOffer = GetByID(entityToUpdate.Id);
		    foreach (var timeSlot in existingOffer.TimeSlots)
		    {
		        if (entityToUpdate.TimeSlots.All(x => x.Id != timeSlot.Id))
                    TimeSlots.Delete(_timeSlotsMapper.UnMap(timeSlot));
		    }
            
		    foreach (var timeSlot in entityToUpdate.TimeSlots)
		    {
                if (timeSlot.Id == default(int))
                    TimeSlots.Insert(_timeSlotsMapper.UnMap(timeSlot));
                else
                    TimeSlots.Update(_timeSlotsMapper.UnMap(timeSlot));
		    }
			Offers.Update(_bookingOffersMapper.UnMap(entityToUpdate));

            _unitOfWork.Save();
		}
	}
}
