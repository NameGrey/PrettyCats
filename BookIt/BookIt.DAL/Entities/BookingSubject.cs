using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingSubject : EntityBase
	{
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
