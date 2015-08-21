using System;
using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Services;

namespace BookIt.BLL.Entities
{
    public class BookingOfferDto
    {
        public int Id { get; set; }
        public int? BookingSubjectId { get; set; }
        public SortedSet<BookingTimeSlotDto> TimeSlots { get; set; }
        public bool IsOccupied { get; set; }
        public UserDto Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInfinite { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CategoryDto Category { get; set; }

        public BookingOfferDto()
        {
            TimeSlots = new SortedSet<BookingTimeSlotDto>(new BookingTimeSlotComparer());
        }
    }
}
