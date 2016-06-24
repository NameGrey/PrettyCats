using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PrettyCats.DAL.Entities
{
	public class Pets: IEntity
	{
		private const string HiddenName = "Не отображать";

		public Pets()
		{
			Pictures = new HashSet<Pictures>();
		}

		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(55)]
		public string RussianName { get; set; }

		public int BreedID { get; set; }

		[Column(TypeName = "date")]
		public DateTime? BirthDate { get; set; }

		public string UnderThePictureText { get; set; }

		public int OwnerID { get; set; }

		public int? MotherID { get; set; }

		public int? FatherID { get; set; }

		public int? WhereDisplay { get; set; }

		[StringLength(50)]
		public string Status { get; set; }

		[StringLength(50)]
		public string Color { get; set; }

		public bool IsParent { get; set; }

		public int? Price { get; set; }

		public bool IsInArchive { get; set; }

		[StringLength(50)]
		public string VideoUrl { get; set; }

		[JsonIgnore]
		public virtual Owners Owners { get; set; }

		[JsonIgnore]
		public virtual PetBreeds PetBreeds { get; set; }

		[JsonIgnore]
		public Pets Mother { get; set; }

		[JsonIgnore]
		public Pets Father { get; set; }

		[JsonIgnore]
		public ICollection<Pictures> Pictures { get; set; }
		public DisplayPlaces DisplayPlace { get; set; }

		[NotMapped]
		[JsonIgnore]
		public bool IsHidden => DisplayPlace != null && DisplayPlace.PlaceOfDisplaying != HiddenName;
	}
}
