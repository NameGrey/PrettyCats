using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DbPicturesRepository: IPicturesRepository
	{
		private const string SmallImageFilenameFormat = "{0}_{1}.jpg";
		private const string ImageFilenameFormat = "{0}{1}.jpg";

		private StorageContext _dbContext;

		public DbPicturesRepository(StorageContext context)
		{
			_dbContext = context;
		}

		public DbPicturesRepository() : this(new StorageContext())
		{

		}

		public string GetNewNumberOfImage(string kittenName, bool small = false)
		{
			Pictures firstPicture = _dbContext.Pictures.OrderByDescending(i => i.ID).FirstOrDefault();

			int newNumber = firstPicture != null? firstPicture.ID : 1;

			string format = small ? SmallImageFilenameFormat : ImageFilenameFormat;
			// extract only the fielname
			var fileName = string.Format(format, kittenName, newNumber);

			return fileName;
		}

		public void SetNewOrderForPicture(int id, int newOrder)
		{
			var picture = _dbContext.Pictures.FirstOrDefault(i => i.ID == id);

			if (picture != null)
			{
				picture.Order = newOrder;
			}
		}

		public Pictures GetByID(int id)
		{
			return _dbContext.Pictures.Where(i => i.ID == id).Include(i => i.Pet).FirstOrDefault();
		}

		public IEnumerable<Pictures> GetCollection()
		{
			return _dbContext.Pictures.Include(e=>e.Pet);
		}

		public void Insert(Pictures picture)
		{
			_dbContext.Pictures.Add(picture);
		}

		public void Delete(int id)
		{
			_dbContext.Pictures.Remove(_dbContext.Pictures.Find(id));
		}

		public void Update(Pictures pet)
		{
			throw new System.NotImplementedException();
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}