
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PrettyCats.Database
{
	public class DbStorage
	{
		private readonly static object lockObj = new object();

		private static readonly Storage dbContext;
		public const string KittensImageDirectoryPath = "~/Resources/Kittens";
		public const string SmallImageHorizontal = "small-images-true-size-hor";
		public const string SmallImageVertical = "small-images-true-size-ver";
		private const string SmallImageFilenameFormat = "{0}_{1}.jpg";
		private const string ImageFilenameFormat = "{0}{1}.jpg";

		#region Memory cache Data

		public static List<DisplayPlaces> DisplayPlaces { get; set; }
		public static List<Owners> Owners { get; set; }
		public static List<Pages> Pages { get; set; }
		public static List<PetBreeds> PetBreeds { get; set; }
		public static List<Pets> Pets { get; set; }
		public static List<Pictures> Pictures { get; set; }

		#endregion

		private DbStorage()
		{

		}

		static DbStorage()
		{
			dbContext = new Storage();

			UpdateMemoryCache();
		}

		#region Cache region

		#region Refresh cache

		public static void UpdateMemoryCache()
		{
			UpdateMemoryCache_DisplayPlaces();
			UpdateMemoryCache_Owners();
			UpdateMemoryCache_Pages();
			UpdateMemoryCache_PetBreeds();
			UpdateMemoryCache_Pets();
			UpdateMemoryCache_Pictures();
		}

		private static void UpdateMemoryCache_DisplayPlaces()
		{
			lock (lockObj)
			{
				DisplayPlaces = dbContext.DisplayPlaces.ToList();
			}
		}
		private static void UpdateMemoryCache_Owners()
		{
			lock (lockObj)
			{
				Owners = dbContext.Owners.ToList();
			}
		}
		private static void UpdateMemoryCache_Pages()
		{
			lock (lockObj)
			{
				Pages = dbContext.Pages.ToList();
			}
		}
		private static void UpdateMemoryCache_PetBreeds()
		{
			lock (lockObj)
			{
				PetBreeds = dbContext.PetBreeds.ToList();
			}
		}
		private static void UpdateMemoryCache_Pets()
		{
			lock (lockObj)
			{
				Pets = dbContext.Pets.ToList();
			}
		}
		private static void UpdateMemoryCache_Pictures()
		{
			lock (lockObj)
			{
				Pictures = dbContext.Pictures.ToList();
			}
		}
		
		#endregion

		#region Manage kitten

		public static void AddNewKitten(Pets newKitten)
		{
			lock (lockObj)
			{
				dbContext.Pets.Add(newKitten);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pets();
		}

		public static void RemoveKitten(Pets kitten)
		{
			lock (lockObj)
			{
				dbContext.Pets.Remove(kitten);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pets();
		}

		public static void EditKitten(Pets kitten)
		{
			lock (lockObj)
			{
				dbContext.Pets.AddOrUpdate(kitten);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pets();
		}

		public static void RemovePicture(Pictures picture)
		{
			lock (lockObj)
			{
				dbContext.Pictures.Remove(picture);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pictures();
		}

		public static Pictures AddPicture(Pictures picture)
		{
			Pictures result = null;

			lock (lockObj)
			{
				result = dbContext.Pictures.Add(picture);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pictures();

			return result;
		}

		public static void AddPictureForTheKitten(string kittenName, Pictures picture)
		{
			lock (lockObj)
			{
				dbContext.Pets.First(i => i.Name == kittenName).Pictures.Add(picture);
				dbContext.SaveChanges();
			}

			UpdateMemoryCache_Pictures();
			UpdateMemoryCache_Pets();
		}

		#endregion

		#endregion

		public static Pets GetKittenByName(string name)
		{
			return (from el in Pets where el.Name == name select el).FirstOrDefault();
		}

		public static Pets GetKittenByID(int id)
		{
			return Pets.Find(i => i.ID == id);
		}

		public static string GetKittenImagePath(string kittenName, bool withExtension = true, bool withNamedFolder = false)
		{
			// extract only the fielname
			var fileName = kittenName + (withNamedFolder ? "\\" + kittenName + "\\" : String.Empty) +
							(withExtension ? ".jpg" : String.Empty);
			// store the file inside /Resources/Kittens folder
			var path = Path.Combine(KittensImageDirectoryPath + (withNamedFolder ? "\\" + kittenName + "\\" : String.Empty), fileName);

			return path;
		}

		public static IEnumerable<Pets> GetKittensByBreed(int breedId, bool isInArhive = false)
		{
			return from pet in Pets
				where pet.BreedID == breedId && !pet.IsParent && pet.IsInArchive == isInArhive && pet.WhereDisplay != 3
				select pet;
		}

		public static string GetNumberedImage(string kittenName, bool small = false)
		{
			int newNumber = Pictures.OrderByDescending(i => i.ID).First().ID + 1;

			string format = small ? SmallImageFilenameFormat : ImageFilenameFormat;
			// extract only the fielname
			var fileName = String.Format(format, kittenName, newNumber);

			return fileName;
		}

		public static void SetNewOrderForPicture(int id, int newOrder)
		{
			var picture = Pictures.FirstOrDefault(i => i.ID == id);

			if (picture != null)
			{
				picture.Order = newOrder;
			}
		}

		public static string GetSmallKittenImageFileName(string imagePath)
		{
			string result = String.Empty;
			string name = Path.GetFileNameWithoutExtension(imagePath);

			if (name != null)
			{
				result = Regex.Match(name, @"\d+").Value;
				result = Regex.Replace(imagePath, result, "_" + result);
			}
			
			return result;
		}

		public static bool IsKittenExistsWithAnotherId(Pets kitten)
		{
			return Pets.Any(i => i.Name == kitten.Name && i.ID != kitten.ID);
		}

		public static bool IsKittenExists(Pets kitten)
		{
			return Pets.Any(i => i.Name == kitten.Name);
		}

		public static List<Pets> GetAllParents()
		{
			return (from i in Pets where i.IsParent select i).ToList();
		}

		public static bool IsKittenExistsWithParent(Pets parent)
		{
			return Pets.Any(i => i.MotherID == parent.ID || i.FatherID == parent.ID);
		}
	}
}