using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingSubject
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Count { get; set; }
		public Category Category { get; set; }

		public int OwnerID { get; set; }

		[ForeignKey("OwnerID")]
		public virtual Person Owner { get; set; }
		public virtual ICollection<BookingOffer> BookingOffers { get; set; }
	}
}
