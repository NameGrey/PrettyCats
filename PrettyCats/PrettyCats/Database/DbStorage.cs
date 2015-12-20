
using System.IO;
using System.Linq;
using System.Web;
using PrettyCats.Models;

namespace PrettyCats.Database
{
	public class DbStorage
	{
		public static Storage Instance { get; private set; }

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

		public static string GetKittenImagePath(string kittenName, HttpServerUtilityBase server)
		{
			// extract only the fielname
			var fileName = kittenName + ".jpg";
			// store the file inside /Resources/Kittens folder
			var path = Path.Combine("~/Resources/Kittens", fileName);

			return path;
		}

		public static bool IsKittenExistsWithAnotherId(Pets kitten)
		{
			return Instance.Pets.Any(i => i.Name == kitten.Name && i.ID != kitten.ID);
		}

		public static bool IsKittenExists(Pets kitten)
		{
			return Instance.Pets.Any(i => i.Name == kitten.Name);
		}
	}
}