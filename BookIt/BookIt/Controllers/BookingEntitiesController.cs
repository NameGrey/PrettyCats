using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookIt.Models;

namespace BookIt.Controllers
{
    public class BookingEntitiesController : ApiController
    {
        BookingEntity[] bookingEntities = new BookingEntity[] 
        { 
            new BookingEntity { Id = 1, IsOccupied = true, CategoryId = 1, Name = "Объект 1" }, 
            new BookingEntity { Id = 2, IsOccupied = false, CategoryId = 1, Name = "Объект 2" }, 
            new BookingEntity { Id = 3, IsOccupied = false, CategoryId = 2, Name = "Объект 3" } 
        };

        public IEnumerable<BookingEntity> GetBookingEntities()
        {
            return bookingEntities;
        }

    }
}
