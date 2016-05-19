using System.Collections.Generic;
using PrettyCats.DAL;

namespace PrettyCats.Models
{
	public class KittenModelView
	{
		public string BirthDate { get; set; }

		public string RussianName { get; set; }

		public int? MotherID { get; set; }

		public string MotherName { get; set; }

		public int? FatherID { get; set; }

		public string FatherName { get; set; }

		public string BreedName { get; set; }

		public int BreedID { get; set; }

		public string Color { get; set; }

		public string OwnerName { get; set; }

		public string OwnerPhone { get; set; }

		public string UnderThePictureText { get; set; }

		public bool IsInArchive { get; set; }

		public string VideoUrl { get; set; }

		public int? Price { get; set; }

		public string Status { get; set; }

		public bool IsParent { get; set; }

		public string MainImageSmallSizeUrl { get; set; }

		public string MainImageStandartSizeUrl { get; set; }

		public string MainImageCssClass { get; set; }

		public IEnumerable<Pictures> Pictures { get; set; }
	}
}