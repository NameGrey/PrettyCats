using BookIt.DAL.Entities;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// Mapping Role to BLL.Role
	/// </summary>
	public class RoleMapper : IMapper<BLL.Entities.Role, Role>
	{
		public Role UnMap(BLL.Entities.Role source)
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

		public BLL.Entities.Role Map(Role source)
		{
			if (source == null)
				return null;

			BLL.Entities.Role result = new BLL.Entities.Role()
			{
				Id = source.ID,
				Name = source.Name,
				Description = source.Description,
			};

			return result;
		}
	}
}
