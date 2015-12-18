using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using PrettyCats.Database;
using PrettyCats.Helpers;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	public class AdminController : Controller
	{
		// GET: Admin
		public ActionResult Index()
		{
			return View("Admin");
		}

		public ActionResult AdminPanel()
		{
			return View();
		}

		public ActionResult AdminChangeKittens()
		{
			var v = DbStorage.Instance.Pets.ToList();
			return View();
		}


		public ActionResult KittenOnTheAdminPageHtml()
		{
			return View();
		}

		public ActionResult LogIn(string username, string password)
		{
			if (SecurityHelper.LogInAdmin(username, password))
				return RedirectToAction("AdminPanel");
			else
				return View("Admin", (object) "Неверный пароль!");
		}

		[HttpPost]
		public ActionResult AddKitten(KittenModelView newKitten)
		{
			string result = SaveImage(newKitten);

			DbStorage.Instance.AddNewPet(newKitten, result);
			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminPanel");
		}

		[HttpGet]
		public ActionResult AddKitten()
		{
			KittenModelView newKitten = new KittenModelView
			{
				Name = "newName",
				RussianName = "dff",
				DisplayPlaceId = 1,
				BreedId = 1,
				OwnerId = 1,
				BirthDate = DateTime.Now,
				UnderThePictureText = "Text under the picture",
				Breeds = from el in DbStorage.Instance.PetBreeds.AsEnumerable() select new BreedModelView(el.ID, el.RussianName),
				DisplayPlaces =
					from el in DbStorage.Instance.DisplayPlaces.AsEnumerable()
					select new DisplayPlaceModelView(el.ID, el.PlaceOfDisplaying),
				Owners = from el in DbStorage.Instance.Owners.AsEnumerable() select new OwnerModelView(el.ID, el.Name),
			};


			return View(newKitten);
		}

		private string SaveImage(KittenModelView kitten)
		{
			string path = String.Empty;
			string kittenName = kitten.Name;
			HttpPostedFileBase file = kitten.ImageUpload;

			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0)
			{
				path = DbStorage.Instance.GetKittenImagePath(kittenName, Server);
				RemoveFile(path);
				file.SaveAs(path);
			}

			return path;
		}

		private void RemoveFile(string path)
		{
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
		}
	}
}