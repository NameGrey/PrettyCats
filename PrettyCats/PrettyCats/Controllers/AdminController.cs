using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI;
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
			return View("AdminChangeKittens", v);
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

		public ActionResult AddMainFoto(HttpPostedFileBase f, string kittenName)
		{
			string path = SaveImage(kittenName, f);

			if (!String.IsNullOrEmpty(path))
			{
				var pict = DbStorage.Instance.Pictures.Add(new Pictures() {Image = path});
				DbStorage.Instance.SaveChanges();

				Pets kitten = DbStorage.GetKittenByName(kittenName);
				kitten.PictureID = pict.ID;
				DbStorage.Instance.SaveChanges();
			}

			return RedirectToAction("AdminChangeKittens");
		}


		[HttpPost]
		public ActionResult AddKitten(Pets newKitten)
		{
			if (DbStorage.IsKittenExistsWithAnotherId(newKitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			// Initialize addition fields
			newKitten.Owners = DbStorage.Instance.Owners.First(i => i.ID == newKitten.OwnerID);
			newKitten.PetBreeds = DbStorage.Instance.PetBreeds.First(i => i.ID == newKitten.BreedID);

			DbStorage.Instance.Pets.Add(newKitten);
			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
		}

		public ActionResult Error(string errorText)
		{
			return View("Error", (object)errorText);
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


			return View(new Pets());
		}

		[HttpGet]
		public ActionResult EditKitten(int id)
		{
			Pets kitten = DbStorage.GetKittenByID(id);

			if (kitten == null)
				return RedirectToAction("AdminChangeKittens");
			
			return View(kitten);
		}

		public ActionResult RemoveKitten(int id)
		{
			Pets kitten = DbStorage.GetKittenByID(id);
			if (!DbStorage.IsKittenExists(kitten))
			{
				return Error("Котенка с таким именем не существует!!!");
			}

			DbStorage.Instance.Pets.Remove(kitten);
			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
		}

		[HttpPost]
		public ActionResult EditKitten(Pets kitten)
		{
			if (DbStorage.IsKittenExistsWithAnotherId(kitten))
			{
				return Error("Котенок с таким именем уже есть!!!");
			}

			// Initialize addition fields
			kitten.Owners = DbStorage.Instance.Owners.First(i => i.ID == kitten.OwnerID);
			kitten.PetBreeds = DbStorage.Instance.PetBreeds.First(i => i.ID == kitten.BreedID);
			DbStorage.Instance.Pets.AddOrUpdate(kitten); 

			DbStorage.Instance.SaveChanges();

			return RedirectToAction("AdminChangeKittens");
		}

		private string SaveImage(string kittenName, HttpPostedFileBase file)
		{
			string path = String.Empty;
			
			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0 && !String.IsNullOrEmpty(kittenName))
			{
				path = DbStorage.GetKittenImagePath(kittenName, Server);
				RemoveFile(path);
				file.SaveAs(Server.MapPath(path));
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