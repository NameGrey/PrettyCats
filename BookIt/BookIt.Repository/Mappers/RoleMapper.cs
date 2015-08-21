using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// Mapping Role to BLL.Role
	/// </summary>
	public static class RoleMapper
	{
		public static Role UnMap(RoleDto source)
		{
			if (source == null)
				return null;

			Role result = new Role
			{
				ID = source.Id,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}

		public static RoleDto Map(Role source)
		{
			if (source == null)
				return null;

			RoleDto result = new RoleDto()
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}
	}
}
