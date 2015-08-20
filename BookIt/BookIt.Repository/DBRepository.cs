﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL.Entities;
using BookIt.DAL;
using BookIt.Repository.Mappers;
using BookingOffer = BookIt.DAL.BookingOffer;
using BookingSubject = BookIt.DAL.BookingSubject;
using Category = BookIt.BLL.Entities.CategoryTypes;
using Person = BookIt.BLL.Entities.UserDto;

namespace BookIt.Repository
{
	public class DBRepository : IBookItRepository
	{
		private BookingContext dbContext;
		private PersonsMapper personMapper;
		private BookingSubjectsMapper bookingSubjectsMapper;
		private BookingOffersMapper bookingOffersMapper;
		private TimeSlotsMapper timeSlotsMapper;
		private CategoriesMapper categoriesMapper;

		public DBRepository()
		{
			dbContext = new BookingContext();
			personMapper = new PersonsMapper();
			bookingSubjectsMapper = new BookingSubjectsMapper();
			bookingOffersMapper = new BookingOffersMapper();
			timeSlotsMapper = new TimeSlotsMapper();
			categoriesMapper = new CategoriesMapper();
		}

		#region IBookItRepository Members

		public IEnumerable<Person> GetPersons()
		{
			return personMapper.MapAll(dbContext.Persons);
		}

		public IEnumerable<BLL.Entities.BookingSubjectDto> GetAllBookingSubjects()
		{
			return bookingSubjectsMapper.MapAll(dbContext.BookingSubjects);
		}

		public IEnumerable<BLL.Entities.BookingOfferDto> GetAllBookingOffers()
		{
			return bookingOffersMapper.MapAll(dbContext.BookingOffers);
		}

		public void CreateBookingOffer(BLL.Entities.BookingOfferDto offer)
		{
			var dbOffer = new BookingOffer();
			bookingOffersMapper.UnMap(offer, dbOffer);
			dbContext.BookingOffers.Add(dbOffer);
			UpdateTimeSlots(offer, dbOffer);
			dbContext.SaveChanges();
		}

		public void CreateBookingSubject(BLL.Entities.BookingSubjectDto subject)
		{
			var dbSubject = new BookingSubject();
			bookingSubjectsMapper.UnMap(subject, dbSubject);
			dbContext.BookingSubjects.Add(dbSubject);
			dbContext.SaveChanges();
		}

		public void UpdateBookingOffer(BLL.Entities.BookingOfferDto bookingOffer)
		{
			if (bookingOffer != null)
			{
				var dbOffer = dbContext.BookingOffers.FirstOrDefault(o => o.ID == bookingOffer.Id);
				if (dbOffer != null)
				{
					bookingOffersMapper.UnMap(bookingOffer, dbOffer);
					UpdateTimeSlots(bookingOffer, dbOffer);
					try
					{
						dbContext.SaveChanges();
					}
					catch (Exception e)
					{

					}
				}
				else
					throw new ArgumentOutOfRangeException();
			}

		}

		#endregion

		private void UpdateTimeSlots(BLL.Entities.BookingOfferDto offer, BookingOffer dbOffer)
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
					foreach (BookingTimeSlotDto slot in offer.TimeSlots)
					{
						var dbSLot = dbOfferSlots.FirstOrDefault(s => s.ID == slot.Id);
						if (dbSLot == null)//если слот новый - добавляем его
						{
							dbSLot = new TimeSlot();
							dbContext.TimeSlots.Add(dbSLot);

						}
						else
						{
							dbOfferSlots.Remove(dbSLot);//удаляем из копии слотов обработанные слоты
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

		#region IBookItRepository Members

		public IEnumerable<Category> GetCategories()
		{
			return categoriesMapper.MapAll();
		}

		#endregion
	}
}
