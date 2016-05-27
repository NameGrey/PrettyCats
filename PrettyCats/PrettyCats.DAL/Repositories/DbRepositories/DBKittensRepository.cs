using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories.DbRepositories
{
	public class DBKittensRepository: IKittensRepository
	{
		private StorageContext dbContext;
		public const string SmallImageHorizontal = "small-images-true-size-hor";
		public const string SmallImageVertical = "small-images-true-size-ver";

		public DBKittensRepository(StorageContext context)
		{
			dbContext = context;
		}

		#region IKittensRepository

			#region IReadOnlyRepository

			public Pets GetByID(int id)
			{
				return dbContext.Pets.Find(id);
			}

			public IEnumerable<Pets> GetCollection()
			{
				return dbContext.Pets.Include(b=>b.Pictures);
			}

			#endregion

			#region IRepository

			public void Insert(Pets pet)
			{
				dbContext.Pets.Add(pet);
			}

			public void Delete(int id)
			{
				dbContext.Pets.Remove(dbContext.Pets.Find(id));
			}

			public void Update(Pets pet)
			{
				dbContext.Pets.AddOrUpdate(pet);
			}

			public void Save()
			{
				dbContext.SaveChanges();
			}

			#endregion

		#endregion

		public Pets GetKittenByName(string name)
		{
			return (from el in dbContext.Pets where el.Name == name select el).FirstOrDefault();
		}

		public IEnumerable<Pets> GetKittensByBreed(int breedId, bool isInArhive = false)
		{
			return from pet in dbContext.Pets
				where pet.BreedID == breedId && !pet.IsParent && pet.IsInArchive == isInArhive && pet.WhereDisplay != 3
				select pet;
		}

		public bool IsKittenExistsWithAnotherId(Pets kitten)
		{
			return dbContext.Pets.Any(i => i.Name == kitten.Name && i.ID != kitten.ID);
		}

		public bool IsKittenExists(Pets kitten)
		{
			return dbContext.Pets.Any(i => i.Name == kitten.Name);
		}

		public List<Pets> GetAllParents()
		{
			return (from i in dbContext.Pets where i.IsParent select i).ToList();
		}

		public bool IsKittenExistsWithParent(Pets parent)
		{
			return dbContext.Pets.Any(i => i.MotherID == parent.ID || i.FatherID == parent.ID);
		}

		public void AddPictureForTheKitten(string kittenName, Pictures picture)
		{
			var kitten = dbContext.Pets.FirstOrDefault(i => i.Name == kittenName);

			kitten?.Pictures.Add(picture);
		}
	}
}