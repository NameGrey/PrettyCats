using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Enteties
{
	public class Pictures: IEntity
	{
		public Pictures()
		{
			Pets = new HashSet<Pets>();
		}

		public int ID { get; set; }

		public string Image { get; set; }

		public string ImageSmall { get; set; }

		[StringLength(50)]
		public string CssClass { get; set; }

		public int Order { get; set; }

		public virtual ICollection<Pets> Pets { get; set; }
	}
}
