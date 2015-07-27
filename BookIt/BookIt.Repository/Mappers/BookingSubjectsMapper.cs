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
		public override BLL.BookingSubject Map(BookingSubject entity)
		{
			var bllBookingSubject = new BLL.BookingSubject();
			if (entity != null)
			{
				bllBookingSubject.Id = entity.ID;
				bllBookingSubject.Name = entity.Name;
				bllBookingSubject.Capacity = entity.Count;
				bllBookingSubject.CategoryId = (int)entity.Category;
				bllBookingSubject.Owner = new PersonMapper().Map(entity.Owner);
			}
			return bllBookingSubject;
		}
	}
}
