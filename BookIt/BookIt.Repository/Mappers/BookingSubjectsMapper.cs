using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class BookingSubjectsMapper:MapperBase<BLL.BookingSubject, BookingSubject>
	{
		public override void UnMap(BLL.BookingSubject bookingSubject, BookingSubject dbBookingSubject)
		{
			dbBookingSubject.ID = bookingSubject.Id;
			dbBookingSubject.Category = (Category)Enum.ToObject(typeof(Category), bookingSubject.CategoryId);
			dbBookingSubject.Count = bookingSubject.Capacity;
#warning dbEntity.Description
			dbBookingSubject.Name = bookingSubject.Name;
			dbBookingSubject.OwnerID = bookingSubject.Owner.Id;
		}

		public override BLL.BookingSubject Map(BookingSubject dbBookingSubject)
		{
			var bllBookingSubject = new BLL.BookingSubject();
			if (dbBookingSubject != null)
			{
				bllBookingSubject.Id = dbBookingSubject.ID;
				bllBookingSubject.Name = dbBookingSubject.Name;
				bllBookingSubject.Capacity = dbBookingSubject.Count;
				bllBookingSubject.CategoryId = (int)dbBookingSubject.Category;
				bllBookingSubject.Owner = new PersonsMapper().Map(dbBookingSubject.Owner);
			}
			return bllBookingSubject;
		}
	}
}
