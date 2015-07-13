using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class Category
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Good> Goods { get; set; }
	}
}
