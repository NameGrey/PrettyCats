namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0135287_serg.Owners")]
    public partial class Owners
    {
        public Owners()
        {
            Pets = new HashSet<Pets>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
