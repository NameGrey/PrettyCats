using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Enteties
{
	public class Pictures: IEntity
	{
		public int ID { get; set; }

		public string Image { get; set; }

		public string ImageSmall { get; set; }

		[StringLength(50)]
		public string CssClass { get; set; }

		public int Order { get; set; }

		public bool IsMainPicture { get; set; }

		public int PetID { get; set; }

		public Pets Pet { get; set; }
	}
}
