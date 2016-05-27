using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbBreedsRepository : IKittenBreedRepository
	{
		private StorageContext _dbContext;

		public DbBreedsRepository(StorageContext context)
		{
			_dbContext = context;
		}

		public PetBreeds GetByID(int id)
		{
			return _dbContext.PetBreeds.Find(id);
		}

		public IEnumerable<PetBreeds> GetCollection()
		{
			return _dbContext.PetBreeds;
		}
	}
}