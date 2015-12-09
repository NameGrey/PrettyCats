using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PrettyCats.Database;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : Controller
	{
		public ActionResult KittenMainPage(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult ParentCatMainPage()
		{
			return View();
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult AllAvailableKittens()
		{
			List<Pets> pets = new List<Pets>();

			pets.Add(new Pets()
			{
				BirthDate = new DateTime(2000, 12, 5),
				BreedID = 1,
				FatherID = null,
				ID = 1,
				MotherID = null,
				Name = "First",
				OwnerID = 1
			});

			pets.Add(new Pets()
			{
				BirthDate = new DateTime(2000, 12, 5),
				BreedID = 2,
				FatherID = null,
				ID = 2,
				MotherID = null,
				Name = "Second",
				OwnerID = 1
			});
			
			return View(pets);
		}
	}
}