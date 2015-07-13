using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class User
	{
		public int ID { get; set; }

		public string FirstName { get; set; }
		
		public string LastName { get; set; }

		public string Email { get; set; }

		public virtual ICollection<Good> PersonalGoods { get; set; }
	}
}
