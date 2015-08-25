using System;

namespace BookIt.BLL.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public int BookingOfferId { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Owner { get; set; }
        public bool IsOccupied { get; set; }
    }
}