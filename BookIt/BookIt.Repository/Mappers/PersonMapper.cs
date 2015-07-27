using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class PersonMapper:MapperBase<BLL.Person,Person>
	{
		public override BLL.Person Map(Person entity)
		{
			var bllPerson = new BLL.Person();
			if (entity != null)
			{
				bllPerson.Id = entity.ID;
				bllPerson.FirstName = entity.FirstName;
				bllPerson.LastName = entity.LastName;
				if (entity.Role == Role.Administrator)
					bllPerson.PersonRole = BLL.Role.Administrator;
				else
					bllPerson.PersonRole = BLL.Role.User;

			}
			return bllPerson;
		}
	}
}
