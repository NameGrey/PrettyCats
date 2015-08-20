using System;

namespace BookIt.BLL.Entities
{
    public class BookingTimeSlotDto
    {
        public int Id { get; set; }
        public int BookingOfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDto Person { get; set; }
        public bool IsOccupied { get; set; }
    }
}