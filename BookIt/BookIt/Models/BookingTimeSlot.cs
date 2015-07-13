using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookIt.Models
{
    public class BookingTimeSlot
    {
        public int Id;
        public DateTime StartDate;
        public DateTime EndDate;
        public Person Person;
    }
}