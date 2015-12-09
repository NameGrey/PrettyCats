namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pets
    {
        public Pets()
        {
            Pets11 = new HashSet<Pets>();
            Pets12 = new HashSet<Pets>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string RussianName { get; set; }

        public int BreedID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public string UnderThePictureText { get; set; }

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

        public virtual Pets Pets1 { get; set; }

        public virtual Pets Pets2 { get; set; }

        public virtual ICollection<Pets> Pets11 { get; set; }

        public virtual Pets Pets3 { get; set; }

        public virtual ICollection<Pets> Pets12 { get; set; }

        public virtual Pets Pets4 { get; set; }
    }
}
