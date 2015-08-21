using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class DBRepository : IBookItRepository
	{
		private readonly BookingContext _dbContext;

		public DBRepository()
		{
			_dbContext = new BookingContext();;
		}

		#region IBookItRepository Members

		public IEnumerable<UserDto> GetUsers()
		{
			return _dbContext.Users.Select(UserMapper.Map).ToList();
		}

		public IEnumerable<BookingSubjectDto> GetSubjects()
		{
			return _dbContext.BookingSubjects.Select(BookingSubjectsMapper.Map).ToList();
		}

		public IEnumerable<BookingOfferDto> GetOffers()
		{
			return _dbContext.BookingOffers.Select(BookingOffersMapper.Map).ToList();
		}

		public void CreateOffer(BookingOfferDto offer)
		{
			var dbOffer = BookingOffersMapper.UnMap(offer);
			if (dbOffer == null)
				return;

			_dbContext.BookingOffers.Add(dbOffer);
			UpdateTimeSlots(offer, dbOffer);
			_dbContext.SaveChanges();
		}

		public void CreateSubject(BookingSubjectDto subject)
		{
			var dbSubject = BookingSubjectsMapper.UnMap(subject);
			if (dbSubject == null)
				return;

			_dbContext.BookingSubjects.Add(dbSubject);
			_dbContext.SaveChanges();
		}

		public void UpdateOffer(BookingOfferDto bookingOffer)
		{
			if (bookingOffer == null) 
				return;

			var dbOffer = _dbContext.BookingOffers.FirstOrDefault(x => x.ID == bookingOffer.Id);
			if (dbOffer != null)
			{
				UpdateTimeSlots(bookingOffer, dbOffer);
				_dbContext.SaveChanges();
			}
			else
				throw new ArgumentOutOfRangeException();
		}

		#endregion

		private void UpdateTimeSlots(BookingOfferDto offer, BookingOffer dbOffer)
		{
			if (offer == null || dbOffer == null)
				return;

			if (offer.TimeSlots.Count == 0)
			{
				RemoveTimeSlots(dbOffer.TimeSlots);
				return;
			}

			var dbOfferSlots = new List<TimeSlot>();
			if (dbOffer.TimeSlots != null && dbOffer.TimeSlots.Count > 0)
			{
				dbOfferSlots = dbOffer.TimeSlots.ToList();
			} 
			
			//делаем копию слотов для оффера
			foreach (BookingTimeSlotDto slot in offer.TimeSlots)
			{
				var dbSlot = dbOfferSlots.FirstOrDefault(s => s.ID == slot.Id);

				if (dbSlot == null) //если слот новый - добавляем его
				{
					_dbContext.TimeSlots.Add(TimeSlotsMapper.UnMap(slot));
				}
				else
				{
					dbOfferSlots.Remove(dbSlot); //удаляем из копии слотов обработанные слоты
					_dbContext.TimeSlots.Remove(dbSlot);
					_dbContext.TimeSlots.Add(TimeSlotsMapper.UnMap(slot));
				}
			}

			RemoveTimeSlots(dbOfferSlots);
		}

		private void RemoveTimeSlots(IEnumerable<TimeSlot> timeSlots)
		{
			if (timeSlots != null && timeSlots.Any())
			{
				_dbContext.TimeSlots.RemoveRange(timeSlots);
				_dbContext.SaveChanges();
			}
		}

		#region IBookItRepository Members

		public IEnumerable<CategoryDto> GetCategories()
		{
			return _dbContext.Categories.Select(CategoriesMapper.Map).ToList();
		}

		public IEnumerable<RoleDto> GetRoles()
		{
			return _dbContext.Roles.Select(RoleMapper.Map).ToList();
		}

		#endregion
	}
}
