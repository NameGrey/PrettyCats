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
            Pictures = new HashSet<Pictures>();
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

        public int? PictureID { get; set; }

        public int? WhereDisplay { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public bool IsParent { get; set; }

        public virtual Owners Owners { get; set; }

        public virtual PetBreeds PetBreeds { get; set; }

        public virtual ICollection<Pictures> Pictures { get; set; }
    }
}
