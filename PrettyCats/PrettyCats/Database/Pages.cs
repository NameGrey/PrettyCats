namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pages
    {
        public int ID { get; set; }

        public string Content { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
