namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pictures
    {
        public Pictures()
        {
            Pets = new HashSet<Pets>();
        }

        public int ID { get; set; }

        public string Image { get; set; }

        public string ImageSmall { get; set; }

        [StringLength(50)]
        public string CssClass { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
