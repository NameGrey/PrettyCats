
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Ajax.Utilities;
using PrettyCats.Models;

namespace PrettyCats.Database
{
	public class DbStorage
	{
		public static Storage Instance { get; private set; }
		public const string KittensImageDirectoryPath = "~/Resources/Kittens";
		public const string SmallImageHorizontal = "small-images-true-size-hor";
		public const string SmallImageVertical = "small-images-true-size-ver";
		private const string SmallImageFilenameFormat = "{0}_{1}.jpg";
		private const string ImageFilenameFormat = "{0}{1}.jpg";

		private DbStorage()
		{

		}

		static DbStorage()
		{
			Instance = new Storage();
		}

		public static void AddNewPet(KittenModelView newKitten, string imagePath)
		{
			if (newKitten.ImageUpload.ContentLength > 0)
			{
				var newPicture = Instance.Pictures.Add(new Pictures() { Image = imagePath });
			}

			Instance.Pets.Add(new Pets()
			{
				Name = newKitten.Name,
				RussianName = newKitten.RussianName,
				BirthDate = newKitten.BirthDate,
				UnderThePictureText = newKitten.UnderThePictureText,
				BreedID = newKitten.BreedId,
				WhereDisplay = newKitten.DisplayPlaceId,
				OwnerID = newKitten.OwnerId
			});
		}

		public static void UpdateKitten(Pets kitten)
		{
			//Instance.Pets.Find(kitten.ID).
		}

		public static Pets GetKittenByName(string name)
		{
			return (from el in Instance.Pets where el.Name == name select el).FirstOrDefault();
		}

		public static Pets GetKittenByID(int id)
		{
			return Instance.Pets.Find(id);
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
			return from pet in Instance.Pets
				where pet.BreedID == breedId && !pet.IsParent && pet.IsInArchive == isInArhive && pet.WhereDisplay != 3
				select pet;
		}

		public static string GetNumberedImage(string kittenName, bool small = false)
		{
			int newNumber = Instance.Pictures.OrderByDescending(i => i.ID).First().ID + 1;

			string format = small ? SmallImageFilenameFormat : ImageFilenameFormat;
			// extract only the fielname
			var fileName = String.Format(format, kittenName, newNumber);

			return fileName;
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
			return Instance.Pets.Any(i => i.Name == kitten.Name && i.ID != kitten.ID);
		}

		public static bool IsKittenExists(Pets kitten)
		{
			return Instance.Pets.Any(i => i.Name == kitten.Name);
		}

		public static List<Pets> GetAllParents()
		{
			return (from i in Instance.Pets where i.IsParent select i).ToList();
		}

		public static bool IsKittenExistsWithParent(Pets parent)
		{
			return Instance.Pets.Any(i => i.MotherID == parent.ID || i.FatherID == parent.ID);
		}
	}
}