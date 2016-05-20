using PrettyCats.DAL.Enteties;

namespace PrettyCats.DAL.Repositories
{
	public interface IRepository<T> : IReadOnlyRepository<T> where T : IEntity
	{
		void Insert(T item);

		void Delete(int id);

		void Update(T pet);

		void Save();
	}
}
