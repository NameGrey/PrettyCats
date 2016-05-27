using System.Collections.Generic;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbDisplayPlacesRepository : IKittenDisplayPlaceRepository
	{
		private StorageContext _dbContext;

		public DbDisplayPlacesRepository(StorageContext context)
		{
			_dbContext = context;
		}

		public DisplayPlaces GetByID(int id)
		{
			return _dbContext.DisplayPlaces.Find(id);
		}

		public IEnumerable<DisplayPlaces> GetCollection()
		{
			return _dbContext.DisplayPlaces;
		}
	}
}