using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.BLL;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class TimeSlotsMapper: MapperBase<BookingTimeSlot,TimeSlot>
	{
		public override void UnMap(BookingTimeSlot bookingTimeSlot, TimeSlot dbTimeSlot)
		{
			dbTimeSlot.ID = bookingTimeSlot.Id;
			dbTimeSlot.BookingOfferID = bookingTimeSlot.BookingOfferId;
			dbTimeSlot.EndDate = bookingTimeSlot.EndDate;
			dbTimeSlot.IsBusy = bookingTimeSlot.IsOccupied;
			if (bookingTimeSlot.Person!=null)
				dbTimeSlot.OwnerID = bookingTimeSlot.Person.Id;
			dbTimeSlot.StartDate = bookingTimeSlot.StartDate;
		}

		public override BookingTimeSlot Map(TimeSlot dbTimeSlot)
		{
			var bllTimeSLot = new BookingTimeSlot();
			if (dbTimeSlot != null)
			{
				bllTimeSLot.BookingOfferId = dbTimeSlot.BookingOfferID;
				bllTimeSLot.EndDate = dbTimeSlot.EndDate;
				bllTimeSLot.Id = dbTimeSlot.ID;
				bllTimeSLot.IsOccupied = dbTimeSlot.IsBusy;
				bllTimeSLot.Person = new PersonsMapper().Map(dbTimeSlot.Owner);
				bllTimeSLot.StartDate = dbTimeSlot.StartDate;
			}
			return bllTimeSLot;
		}


		
	}
}
