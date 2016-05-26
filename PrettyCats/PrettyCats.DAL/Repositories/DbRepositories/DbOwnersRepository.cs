using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbOwnersRepository : IKittenOwnerRepository
	{
		private StorageContext dbContext;

		public DbOwnersRepository(StorageContext context)
		{
			dbContext = context;
		}

		public Owners GetByID(int id)
		{
			return dbContext.Owners.Find(id);
		}

		public IEnumerable<Owners> GetCollection()
		{
			return dbContext.Owners;
		}
	}
}