namespace PrettyCats.Models
{
	public class KittenShortModelView
	{
		public int ID { get; set; }
		public int? PictureID { get; set; }

		public string ImageUrl { get; set; }

		public bool IsParent { get; set; }

		public string RussianName { get; set; }

		public string Status { get; set; }

	}
}