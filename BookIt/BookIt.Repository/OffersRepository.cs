using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;


namespace BookIt.Repository
{
    public class OffersRepository : GenericRepository<BLL.Entities.Offer, DAL.Entities.BookingOffer>, IOffersRepository
    {
       private readonly GenericRepository<BLL.Entities.TimeSlot, DAL.Entities.TimeSlot> _timeSlotsRepository;


        public OffersRepository(): base(new BookingContext(), new BookingOffersMapper())
		{
            _timeSlotsRepository = new GenericRepository<BLL.Entities.TimeSlot, DAL.Entities.TimeSlot>(Context, new TimeSlotsMapper());
		}

		

		public override void Update(Offer entityToUpdate)
		{
            var existingOffer = GetByID(entityToUpdate.Id);
		    UpdateTimeSlots(entityToUpdate.TimeSlots, existingOffer.TimeSlots);
			base.Update(entityToUpdate);

		    Context.SaveChanges();
		}

        public override void Insert(Offer entityToInsert)
        {
            if (!entityToInsert.IsInfinite)
            {
                if (entityToInsert.EndDate == null)
                {
                    throw new ArgumentNullException("Offer with IsInfinite = false without EndDate or StartDate");
                }
                _timeSlotsRepository.Insert(
                    new BLL.Entities.TimeSlot
                        {
                            BookingOfferId = entityToInsert.Id,
                            EndDate = entityToInsert.EndDate.Value,
                            StartDate = entityToInsert.StartDate,
                            IsOccupied = false,
                            Owner = entityToInsert.Owner
                        });
            }

            base.Insert(entityToInsert);
            Context.SaveChanges();
        }

        private void UpdateTimeSlots(IEnumerable<BLL.Entities.TimeSlot> newTimeSlots, IEnumerable<BLL.Entities.TimeSlot> oldTimeSlots)
        {
            List<BLL.Entities.TimeSlot> timeSlotsToUpdate = new List<BLL.Entities.TimeSlot>();
            foreach (var timeSlot in oldTimeSlots)
            {
                bool notExistInNewOffer = newTimeSlots.FirstOrDefault(x => x.Id == timeSlot.Id) == null;

                if (notExistInNewOffer)
                    _timeSlotsRepository.Delete(timeSlot);
                else
                    timeSlotsToUpdate.Add(timeSlot);
            }

            foreach (BLL.Entities.TimeSlot timeSlot in newTimeSlots)
            {
                var existingTimeSlot = timeSlotsToUpdate.FirstOrDefault(x=> x.Id == timeSlot.Id);

                if (existingTimeSlot != null)
                    _timeSlotsRepository.Update(timeSlot);
                else
                    _timeSlotsRepository.Insert(timeSlot);
            }
        }
    }
}
