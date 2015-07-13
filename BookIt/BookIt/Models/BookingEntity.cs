using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookIt.Models
{
    public class BookingEntity
    {
        public int Id;
        public int CategoryId;
        public bool IsOccupied;
        public IEnumerable<BookingTimeSlot> TimeSlots;
        public string Name;
    }
}