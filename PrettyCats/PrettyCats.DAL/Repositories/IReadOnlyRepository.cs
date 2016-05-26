using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories
{
	public interface IReadOnlyRepository<T> where T : IEntity
	{
		T GetByID(int id);

		IEnumerable<T> GetCollection();
	}
}
