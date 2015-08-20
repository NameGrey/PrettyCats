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
	public class PersonsMapper:MapperBase<BLL.Entities.UserDto,Person>
	{
		public override void UnMap(BLL.Entities.UserDto bookingPerson, Person dbPerson)
		{
			dbPerson.ID = bookingPerson.Id;
			dbPerson.FirstName = bookingPerson.FirstName;
			dbPerson.LastName = bookingPerson.LastName;
			if (bookingPerson.Role == BLL.Entities.RoleTypes.Administrator)
				dbPerson.Role = Role.Administrator;
			else
				dbPerson.Role = Role.Employee;
		}

		public override BLL.Entities.UserDto Map(Person dbPerson)
		{
			if (dbPerson == null)
				return null;
			BLL.Entities.UserDto bllPerson = new BLL.Entities.UserDto()
			{

				Id = dbPerson.ID,
				FirstName = dbPerson.FirstName,
				LastName = dbPerson.LastName,
			};
			if (dbPerson.Role == Role.Administrator)
				bllPerson.Role = BLL.Entities.RoleTypes.Administrator;
			else
				bllPerson.Role = BLL.Entities.RoleTypes.User;

			return bllPerson;
		}
	}
}
