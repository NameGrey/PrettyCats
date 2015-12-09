namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PetBreeds
    {
        public PetBreeds()
        {
            Pets = new HashSet<Pets>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string RussianName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
