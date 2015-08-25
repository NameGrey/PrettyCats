using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookIt.DAL.Entities
{
	public class BookingSubject
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public int Capacity { get; set; }

		public int CategoryID { get; set; }
		[ForeignKey("CategoryID")]
		public virtual Category Category { get; set; }

		public int OwnerID { get; set; }
		[ForeignKey("OwnerID")]
		public virtual User Owner { get; set; }

		public virtual ICollection<BookingOffer> BookingOffers { get; set; }
	}
}
