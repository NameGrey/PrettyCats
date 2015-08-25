using System.Collections.Generic;

namespace BookIt.BLL.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
        public User Owner { get; set; }
        public int Capacity { get; set; }

		public ICollection<Offer> BookingOffers { get; set; }

	    public Subject()
	    {
		    BookingOffers = new List<Offer>();
	    }
    }
}