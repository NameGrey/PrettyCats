using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class BookingOfferMapper:MapperBase<BLL.BookingOffer, BookingOffer>
	{

		public override BLL.BookingOffer Map(BookingOffer offer)
		{
			var bllBookingOffer = new BLL.BookingOffer();
			if (offer != null)
			{
				bllBookingOffer.BookingSubjectId = offer.BookingSubjectID;
				bllBookingOffer.Id = offer.ID;
				bllBookingOffer.IsInfinite = offer.IsInfinite;
				bllBookingOffer.SubjectName = offer.Name;
#warning need to discuss
				//bllBookingOffer.IsOccupied
				bllBookingOffer.Owner = new PersonMapper().Map(offer.Owner);
				bllBookingOffer.StartDate = offer.StartDate;
				bllBookingOffer.EndDate = offer.EndDate;
#warning need to add
				//bllBookingOffer.TimeSlots
				
			}
			throw new NotImplementedException();
		}
	}
}
