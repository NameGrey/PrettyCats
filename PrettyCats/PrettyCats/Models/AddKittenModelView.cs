﻿using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.Models
{
	public class AddKittenModelView : KittenModelView
	{
		public int ID { get; set; }
		public int? PictureID { get; set; }
		public string Name { get; set; } // English name of the kitten
		public int OwnerID { get; set; }
		public int? WhereDisplay { get; set; }

		public List<PetBreeds> Breeds { get; set; }
		public List<Owners> Owners { get; set; }
		public List<Pets> AllParents { get; set; }
		public List<DisplayPlaces> DisplayPlaces { get; set; }
	}
}