using System.ComponentModel.DataAnnotations;

namespace PrettyCats.DAL
{
	public partial class DisplayPlaces
	{
		public int ID { get; set; }

		[StringLength(50)]
		public string PlaceOfDisplaying { get; set; }
	}
}
