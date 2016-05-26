using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Entities
{
	public class Owners: IEntity
	{
		public int ID { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(50)]
		public string Phone { get; set; }
	}
}
