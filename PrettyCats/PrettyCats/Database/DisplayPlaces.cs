namespace PrettyCats.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DisplayPlaces
    {
        public DisplayPlaces()
        {
            Pets = new HashSet<Pets>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string PlaceOfDisplaying { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
