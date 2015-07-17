using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookIt.BLL
{
    public class BookingTimeSlot
    {
        public int Id { get; set; }
        public int BookingOfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Person Person { get; set; }
        public bool IsOccupied { get; set; }
    }
}