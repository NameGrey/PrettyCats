using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Enteties
{
	public class Owners: IEntity
	{
		public Owners()
		{
			Pets = new HashSet<Pets>();
		}

		public int ID { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(50)]
		public string Phone { get; set; }

		public virtual ICollection<Pets> Pets { get; set; }
	}
}
