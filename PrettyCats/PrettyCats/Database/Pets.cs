namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0135287_serg.Pets")]
    public partial class Pets
    {
        public Pets()
        {
            Pets1 = new HashSet<Pets>();
            Pets11 = new HashSet<Pets>();
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

        public int? MainPictureID { get; set; }

        public virtual Owners Owners { get; set; }

        public virtual PetBreeds PetBreeds { get; set; }

        public virtual ICollection<Pets> Pets1 { get; set; }

        public virtual Pets Pets2 { get; set; }

        public virtual ICollection<Pets> Pets11 { get; set; }

        public virtual Pets Pets3 { get; set; }
    }
}
