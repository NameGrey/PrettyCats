using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL.Entities
{
	public class DisplayPlaces: IEntity
	{
		public int ID { get; set; }

		[StringLength(50)]
		public string PlaceOfDisplaying { get; set; }
	}
}
