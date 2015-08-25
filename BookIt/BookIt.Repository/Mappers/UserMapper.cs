using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// Mapping Person to BLL.Person
	/// </summary>
	public class UserMapper : IMapper<BLL.Entities.User, User>
	{
		RoleMapper _roleMapper = new RoleMapper();

		public User UnMap(BLL.Entities.User source)
		{
			if (source == null)
				return null;

			User result = new User
			{
				ID = source.Id,
				FirstName = source.FirstName,
				LastName = source.LastName,
				RoleID = source.Role.Id
			};

			return result;
		}

		public BLL.Entities.User Map(User source)
		{
			if (source == null)
				return null;

			BLL.Entities.User result = new BLL.Entities.User
			{
				Id = source.ID,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Role = _roleMapper.Map(source.Role)
			};

			return result;
		}
	}
}
