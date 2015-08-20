using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class BookingOffersMapper:MapperBase<BookingOfferDto, BookingOffer>
	{
		public override void UnMap(BookingOfferDto bookingOffer, BookingOffer dbBookingOffer)
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

		public override BookingOfferDto Map(BookingOffer dbBookingOffer)
		{
			var bllBookingOffer = new BookingOfferDto();
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

			    var timeSlotsDto = new TimeSlotsMapper().MapAll(dbBookingOffer.TimeSlots);
			    foreach (var timeSlot in timeSlotsDto)
			    {
			        bllBookingOffer.TimeSlots.Add(timeSlot);
			    }

			}
			return bllBookingOffer;
		}
	}
}
