using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class BookingOffersMapper:MapperBase<BLL.BookingOffer, BookingOffer>
	{
		public override void UnMap(BLL.BookingOffer bookingOffer, BookingOffer dbBookingOffer)
		{
			dbBookingOffer.ID = bookingOffer.Id;
			dbBookingOffer.BookingSubjectID = bookingOffer.BookingSubjectId;
			dbBookingOffer.Category = new CategoriesMapper().UnMap(bookingOffer.Category);
		
			dbBookingOffer.Description = bookingOffer.Description;
			dbBookingOffer.StartDate = bookingOffer.StartDate;
			dbBookingOffer.EndDate = bookingOffer.EndDate;
			dbBookingOffer.IsInfinite = bookingOffer.IsInfinite;
			dbBookingOffer.Name = bookingOffer.Name;
			dbBookingOffer.OwnerID = bookingOffer.Owner.Id;
		}

		public override BLL.BookingOffer Map(BookingOffer dbBookingOffer)
		{
			var bllBookingOffer = new BLL.BookingOffer();
			if (dbBookingOffer != null)
			{
				bllBookingOffer.BookingSubjectId = dbBookingOffer.BookingSubjectID;
				bllBookingOffer.Id = dbBookingOffer.ID;
				bllBookingOffer.IsInfinite = dbBookingOffer.IsInfinite;
				bllBookingOffer.BookingSubjectId = dbBookingOffer.BookingSubjectID;
				bllBookingOffer.Name = dbBookingOffer.Name;
#warning need to discuss 
				//bllBookingOffer.IsOccupied
				bllBookingOffer.Description = dbBookingOffer.Description;
				bllBookingOffer.Category = new CategoriesMapper().Map(dbBookingOffer.Category);
				bllBookingOffer.Owner = new PersonsMapper().Map(dbBookingOffer.Owner);
				bllBookingOffer.StartDate = dbBookingOffer.StartDate;
				bllBookingOffer.EndDate = dbBookingOffer.EndDate;

				bllBookingOffer.AddTimeSlots(new  TimeSlotsMapper().MapAll(dbBookingOffer.TimeSlots));
				
			}
			return bllBookingOffer;
		}
	}
}
