using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DBKittensRepository: IKittensRepository
	{
		private StorageContext _dbContext;
		public const string SmallImageHorizontal = "small-images-true-size-hor";
		public const string SmallImageVertical = "small-images-true-size-ver";

		public DBKittensRepository(StorageContext context)
		{
			_dbContext = context;
		}

		public DBKittensRepository(): this(new StorageContext())
		{
			
		}

		#region IKittensRepository

			#region IReadOnlyRepository

			public Pets GetByID(int id)
			{
				return _dbContext.Pets.Where(i=>i.ID == id).Include(i=>i.Pictures).FirstOrDefault();
			}

			public IEnumerable<Pets> GetCollection()
			{
				return _dbContext.Pets.Include(b=>b.Pictures).Include(b=>b.PetBreeds);
			}

			#endregion

			#region IRepository

			public void Insert(Pets pet)
			{
				_dbContext.Pets.Add(pet);
			}

			public void Delete(int id)
			{
				_dbContext.Pets.Remove(_dbContext.Pets.Find(id));
			}

			public void Update(Pets pet)
			{
				_dbContext.Pets.AddOrUpdate(pet);
			}

			public void Save()
			{
				_dbContext.SaveChanges();
			}

			#endregion

		#endregion

		public Pets GetKittenByName(string name)
		{
			return _dbContext.Pets.Where(i => i.Name == name).Include(i => i.Pictures).FirstOrDefault();
		}

		public IEnumerable<Pets> GetKittensByBreed(int breedId, bool isInArhive = false)
		{
			return _dbContext.Pets.Where(i => !i.IsParent && i.IsInArchive == isInArhive && i.WhereDisplay != 3 && i.BreedID == breedId).Include(i => i.Pictures);
		}

		public bool IsKittenExistsWithAnotherId(Pets kitten)
		{
			return _dbContext.Pets.Any(i => i.Name == kitten.Name && i.ID != kitten.ID);
		}

		public bool IsKittenExists(Pets kitten)
		{
			return _dbContext.Pets.Any(i => i.Name == kitten.Name);
		}

		public List<Pets> GetAllParents()
		{
			return _dbContext.Pets.Where(i => i.IsParent).Include(i => i.Pictures).ToList();
		}

		public bool IsKittenExistsWithParent(Pets parent)
		{
			return _dbContext.Pets.Any(i => i.MotherID == parent.ID || i.FatherID == parent.ID);
		}

		public void AddPictureForTheKitten(string kittenName, Pictures picture)
		{
			var kitten = _dbContext.Pets.FirstOrDefault(i => i.Name == kittenName);

			kitten?.Pictures.Add(picture);
		}
	}
}