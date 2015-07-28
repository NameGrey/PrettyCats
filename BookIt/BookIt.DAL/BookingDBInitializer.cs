using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DAL
{
	public class BookingDBInitializer : DropCreateDatabaseAlways<BookingContext>
	{
		protected override void Seed(BookingContext context)
		{
			IList<Person> defaultPersons = new List<Person>();

			defaultPersons.Add(new Person() { FirstName = "TestUser1",Role=Role.Administrator });
			defaultPersons.Add(new Person() { FirstName = "TestUser2", Role = Role.Employee });
			defaultPersons.Add(new Person() { FirstName = "TestUser3", Role = Role.Employee });

			foreach (Person prs in defaultPersons)
				context.Persons.Add(prs);

			base.Seed(context);
		}
	}
}
