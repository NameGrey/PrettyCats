using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbOwnersRepository : IKittenOwnerRepository
	{
		private StorageContext _dbContext;

		public DbOwnersRepository(StorageContext context)
		{
			_dbContext = context;
		}

		public DbOwnersRepository() : this(new StorageContext())
		{
			
		}

		public Owners GetByID(int id)
		{
			return _dbContext.Owners.Find(id);
		}

		public IEnumerable<Owners> GetCollection()
		{
			return _dbContext.Owners;
		}
	}
}