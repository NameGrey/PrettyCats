namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0135287_serg.Pictures")]
    public partial class Pictures
    {
        public int ID { get; set; }

        public string Image { get; set; }
    }
}
