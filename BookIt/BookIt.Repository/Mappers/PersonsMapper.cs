using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// Mapping Person to BLL.Person
	/// </summary>
	public class PersonsMapper:MapperBase<BLL.Person,Person>
	{
		public override void UnMap(BLL.Person bookingPerson, Person dbPerson)
		{
			dbPerson.ID = bookingPerson.Id;
			dbPerson.FirstName = bookingPerson.FirstName;
			dbPerson.LastName = bookingPerson.LastName;
			if (bookingPerson.PersonRole == BLL.Role.Administrator)
				dbPerson.Role = Role.Administrator;
			else
				dbPerson.Role = Role.Employee;
		}

		public override BLL.Person Map(Person dbPerson)
		{
			if (dbPerson == null)
				return null;
			BLL.Person bllPerson = new BLL.Person()
			{

				Id = dbPerson.ID,
				FirstName = dbPerson.FirstName,
				LastName = dbPerson.LastName,
			};
			if (dbPerson.Role == Role.Administrator)
				bllPerson.PersonRole = BLL.Role.Administrator;
			else
				bllPerson.PersonRole = BLL.Role.User;

			return bllPerson;
		}
	}
}
