using System.Collections.Generic;
using PrettyCats.DAL.Enteties;

namespace PrettyCats.DAL.Repositories
{
	public class DbDisplayPlacesRepository : IKittenDisplayPlaceRepository
	{
		private StorageContext dbContext;

		public DbDisplayPlacesRepository(StorageContext context)
		{
			dbContext = context;
		}

		public DisplayPlaces GetByID(int id)
		{
			return dbContext.DisplayPlaces.Find(id);
		}

		public IEnumerable<DisplayPlaces> GetCollection()
		{
			return dbContext.DisplayPlaces;
		}
	}
}