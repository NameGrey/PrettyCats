using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// Mapping Person to BLL.Person
	/// </summary>
	public static class UserMapper
	{
		public static User UnMap(UserDto source)
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

		public static UserDto Map(User source)
		{
			if (source == null)
				return null;

			UserDto result = new UserDto
			{
				Id = source.ID,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Role = RoleMapper.Map(source.Role)
			};

			return result;
		}
	}
}
