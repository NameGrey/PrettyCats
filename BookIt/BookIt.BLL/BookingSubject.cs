using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookIt.BLL
{
    public class BookingSubject
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
        public Person Owner { get; set; }
        public int Capacity { get; set; }     
    }
}