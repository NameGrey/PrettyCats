﻿using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbBreedsRepository : IKittenBreedRepository
	{
		private StorageContext dbContext;

		public DbBreedsRepository(StorageContext context)
		{
			dbContext = context;
		}

		public PetBreeds GetByID(int id)
		{
			return dbContext.PetBreeds.Find(id);
		}

		public IEnumerable<PetBreeds> GetCollection()
		{
			return dbContext.PetBreeds;
		}
	}
}