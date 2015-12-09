using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using PrettyCats.Database;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
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

		public ActionResult KittenOnTheMainPageHtml()
		{
			return View();
		}

		public ActionResult ContactsPage()
		{
			return View();
		}
	}
}