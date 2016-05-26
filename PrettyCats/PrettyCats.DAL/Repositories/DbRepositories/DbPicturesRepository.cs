using System.Collections.Generic;
using System.Linq;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbPicturesRepository: IPicturesRepository
	{
		private const string SmallImageFilenameFormat = "{0}_{1}.jpg";
		private const string ImageFilenameFormat = "{0}{1}.jpg";

		private StorageContext dbContext;

		public DbPicturesRepository(StorageContext context)
		{
			dbContext = context;
		}

		public string GetNewNumberOfImage(string kittenName, bool small = false)
		{
			int newNumber = dbContext.Pictures.OrderByDescending(i => i.ID).First().ID + 1;

			string format = small ? SmallImageFilenameFormat : ImageFilenameFormat;
			// extract only the fielname
			var fileName = string.Format(format, kittenName, newNumber);

			return fileName;
		}

		public void SetNewOrderForPicture(int id, int newOrder)
		{
			var picture = dbContext.Pictures.FirstOrDefault(i => i.ID == id);

			if (picture != null)
			{
				picture.Order = newOrder;
			}
		}

		public Pictures GetByID(int id)
		{
			return dbContext.Pictures.Find(id);
		}

		public IEnumerable<Pictures> GetCollection()
		{
			return dbContext.Pictures;
		}

		public void Insert(Pictures picture)
		{
			dbContext.Pictures.Add(picture);
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Pictures pet)
		{
			throw new System.NotImplementedException();
		}

		public void Save()
		{
			dbContext.SaveChanges();
		}
	}
}