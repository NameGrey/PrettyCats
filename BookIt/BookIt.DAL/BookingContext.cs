using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
    public class BookingContext:DbContext
    {
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Good> Goods { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Good>().Property(g => g.OwnerID).IsOptional();

			base.OnModelCreating(modelBuilder);
		}

    }
}
