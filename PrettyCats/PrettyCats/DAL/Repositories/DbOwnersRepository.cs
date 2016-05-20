﻿using System.Collections.Generic;
using PrettyCats.DAL.Enteties;

namespace PrettyCats.DAL.Repositories
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