using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Entities
{
	public class Pages
	{
		public int ID { get; set; }

		public string Content { get; set; }

		[StringLength(50)]
		public string Name { get; set; }
	}
}
