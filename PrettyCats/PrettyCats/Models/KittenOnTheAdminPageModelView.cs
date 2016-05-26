using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.Models
{
	public class KittenOnTheAdminPageModelView
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string RussianName { get; set; }
		public int? PictureID { get; set; }
		public string ImageUrl { get; set; }
		public string PlaceOfDisplaying { get; set; }

		public List<PetBreeds> Breeds { get; set; }
		public List<Owners> Owners { get; set; }
		public List<Pets> AllParents { get; set; }
		public List<DisplayPlaces> DisplayPlaces { get; set; }
	}
}