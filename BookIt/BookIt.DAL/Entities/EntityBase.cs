using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public abstract class EntityBase
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
