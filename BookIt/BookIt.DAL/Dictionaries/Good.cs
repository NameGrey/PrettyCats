using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class Good
	{
		public Guid ID { get; set; }
		

		public int OwnerID { get; set; }
		public virtual User Owner { get; set; }
		
		public int CategoryID { get; set; }

		public virtual Category Category { get; set; }
	}
}
