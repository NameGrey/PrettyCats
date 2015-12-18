using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrettyCats.Models
{
	public class KittenModelView
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string RussianName { get; set; }
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
		[DataType(DataType.MultilineText)]
		public string UnderThePictureText { get; set; }
		public IEnumerable<BreedModelView> Breeds { get; set; }
		public IEnumerable<DisplayPlaceModelView> DisplayPlaces { get; set; }
		public IEnumerable<OwnerModelView> Owners { get; set; }
		public int BreedId { get; set; }
		public int DisplayPlaceId { get; set; }
		public int OwnerId { get; set; }

		[DataType(DataType.Upload)]
		public HttpPostedFileBase ImageUpload { get; set; }
	}
}