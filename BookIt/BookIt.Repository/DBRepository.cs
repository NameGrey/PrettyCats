using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class DBRepository : IBookItRepository
	{
		private BookingContext dbContext;
		private PersonsMapper personMapper;
		private BookingSubjectsMapper bookingSubjectsMapper;
		private BookingOffersMapper bookingOffersMapper;
		private TimeSlotsMapper timeSlotsMapper;

		public DBRepository()
		{
			dbContext = new BookingContext();
			personMapper = new PersonsMapper();
			bookingSubjectsMapper = new BookingSubjectsMapper();
			bookingOffersMapper = new BookingOffersMapper();
			timeSlotsMapper = new TimeSlotsMapper();
		}

		#region IBookItRepository Members

		public IEnumerable<BLL.Person> GetPersons()
		{
			return personMapper.MapAll(dbContext.Persons);
		}

		public IEnumerable<BLL.BookingSubject> GetAllBookingSubjects()
		{
			return bookingSubjectsMapper.MapAll(dbContext.BookingSubjects);
		}

		public IEnumerable<BLL.BookingOffer> GetAllBookingOffers()
		{
			return bookingOffersMapper.MapAll(dbContext.BookingOffers);
		}

		public void CreateBookingOffer(BLL.BookingOffer offer)
		{
			var dbOffer = new BookingOffer();
			bookingOffersMapper.UnMap(offer, dbOffer);
			dbContext.BookingOffers.Add(dbOffer);
			UpdateTimeSlots(offer, dbOffer);
			dbContext.SaveChanges();
		}

		public void UpdateBookingOffer(BLL.BookingOffer bookingOffer)
		{
			if (bookingOffer != null)
			{
				var dbOffer = dbContext.BookingOffers.FirstOrDefault(o => o.ID == bookingOffer.Id);
				if (dbOffer != null)
				{
					bookingOffersMapper.UnMap(bookingOffer, dbOffer);
					UpdateTimeSlots(bookingOffer, dbOffer);
					dbContext.SaveChanges();
				}
				else
					throw new ArgumentOutOfRangeException();
			}

		}

		#endregion

		private void UpdateTimeSlots(BLL.BookingOffer offer, BookingOffer dbOffer)
		{

			if (offer.TimeSlots != null)
			{
				if (offer.TimeSlots.Count > 0)
				{

					var dbOfferSlots = new List<TimeSlot>();
					if (dbOffer.TimeSlots != null)
					{
						dbOfferSlots.AddRange(dbOffer.TimeSlots);
					}//делаем копию слотов для оффера
					foreach (BLL.BookingTimeSlot slot in offer.TimeSlots)
					{
						var dbSLot = dbOfferSlots.FirstOrDefault(s => s.ID == slot.Id);
						if (dbSLot == null)//если слот новый - добавляем его
						{
							dbSLot = new TimeSlot();
							dbContext.TimeSlots.Add(dbSLot);

						}
						else
						{
							dbContext.TimeSlots.Remove(dbSLot);//удаляем из копии слотов обработанные слоты
						}
						timeSlotsMapper.UnMap(slot, dbSLot);
					}
					if (dbOfferSlots.Count > 0)//слоты, которых уже нет а bookingOffer
					{
						foreach (TimeSlot slot in dbOfferSlots)
						{
							dbContext.TimeSlots.Remove(slot);
						}
					}


				}
				else
				{
					if (dbOffer.TimeSlots != null)
						dbContext.TimeSlots.RemoveRange(dbOffer.TimeSlots);
				}
			}
		}
	}
}
