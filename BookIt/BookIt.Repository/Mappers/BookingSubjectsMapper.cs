using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class BookingSubjectsMapper:MapperBase<BLL.Entities.BookingSubjectDto, BookingSubject>
	{
		public override void UnMap(BLL.Entities.BookingSubjectDto bookingSubject, BookingSubject dbBookingSubject)
		{
			dbBookingSubject.ID = bookingSubject.Id;
			dbBookingSubject.Category = new CategoriesMapper().UnMap(bookingSubject.Category);
			dbBookingSubject.Count = bookingSubject.Capacity;
			dbBookingSubject.Description = bookingSubject.Description;
			dbBookingSubject.Name = bookingSubject.Name;
			dbBookingSubject.OwnerID = bookingSubject.Owner.Id;
		}

		public override BLL.Entities.BookingSubjectDto Map(BookingSubject dbBookingSubject)
		{
			var bllBookingSubject = new BLL.Entities.BookingSubjectDto();
			if (dbBookingSubject != null)
			{
				bllBookingSubject.Id = dbBookingSubject.ID;
				bllBookingSubject.Name = dbBookingSubject.Name;
				bllBookingSubject.Capacity = dbBookingSubject.Count;
				bllBookingSubject.Description = dbBookingSubject.Description;
				bllBookingSubject.Category = new CategoriesMapper().Map(dbBookingSubject.Category);
				bllBookingSubject.Owner = new PersonsMapper().Map(dbBookingSubject.Owner);
			}
			return bllBookingSubject;
		}

		
	}
}
