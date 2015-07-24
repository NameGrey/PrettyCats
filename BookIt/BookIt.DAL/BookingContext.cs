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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
		}

    }
}
