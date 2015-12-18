namespace PrettyCats.Database
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class Pets
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(50)]
		public string RussianName { get; set; }

		[Key]
		[Column(Order = 2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int BreedID { get; set; }

		[Column(TypeName = "date")]
		public DateTime? BirthDate { get; set; }

		public string UnderThePictureText { get; set; }

		[Key]
		[Column(Order = 3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OwnerID { get; set; }

		public int? MotherID { get; set; }

		public int? FatherID { get; set; }

		[Column(TypeName = "image")]
		public byte[] ImageData { get; set; }

		[StringLength(50)]
		public string ContentType { get; set; }

		public int? WhereDisplay { get; set; }

		public virtual DisplayPlaces DisplayPlaces { get; set; }

		public virtual Owners Owners { get; set; }

		public virtual PetBreeds PetBreeds { get; set; }
	}
}
