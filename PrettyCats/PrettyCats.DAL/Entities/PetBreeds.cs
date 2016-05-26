using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Entities
{
	public class PetBreeds: IEntity
	{
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(50)]
		public string RussianName { get; set; }

		public string Description { get; set; }
	}
}
