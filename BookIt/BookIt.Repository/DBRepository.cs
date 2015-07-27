using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class DBRepository:IBookItRepository
	{
		private BookingContext dbContext;
		private PersonMapper personMapper;

		public DBRepository()
		{
			dbContext = new BookingContext();
			personMapper = new PersonMapper();
		}

		#region IBookItRepository Members

		public IEnumerable<BLL.Person> GetPersons()
		{
			return personMapper.MapAll(dbContext.Persons); 
		}

		public IEnumerable<BLL.BookingSubject> GetAllBookingSubjects()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BLL.BookingOffer> GetAllBookingOffers()
		{
			throw new NotImplementedException();
		}

		public void SaveBookingOffer(BLL.BookingOffer offer)
		{
			throw new NotImplementedException();
		}

		public void UpdateBookingOffer(BLL.BookingOffer bookingOffer)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
