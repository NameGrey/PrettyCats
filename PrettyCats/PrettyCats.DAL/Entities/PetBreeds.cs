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
		public string ShortName { get; set; }

		public string FullName { get; set; }

		public string LinkPage { get; set; }

		public string PicturePath { get; set; }
	}
}
