using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class GoodBooking
	{
		public Guid ID { get; set; }

		[ForeignKey("Good")]
		public Guid GoodID { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public virtual Good Good { get; set; }
	}
}
