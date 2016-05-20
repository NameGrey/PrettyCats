using System.Collections.Generic;
using PrettyCats.DAL.Enteties;

namespace PrettyCats.DAL.Repositories
{
	public interface IReadOnlyRepository<T> where T : IEntity
	{
		T GetByID(int id);

		IEnumerable<T> GetCollection();
	}
}
