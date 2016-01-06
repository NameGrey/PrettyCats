using System;
using System.Collections.Generic;
using System.Linq;
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

		public ActionResult ParentCatMainPage(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult KittenOnTheMainPageHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult AllAvailableKittens()
		{
			var v = DbStorage.Instance.Pets.ToList();
			return View(v);
		}

		public ActionResult AllParents()
		{
			var v = DbStorage.Instance.Pets.Where(i=>i.IsParent).ToList();
			return View(v);
		}

		public ActionResult BengalKittens()
		{
			var v = from pet in DbStorage.Instance.Pets where pet.BreedID == 2 && !pet.IsParent select pet;
			return View(v.ToList());
		}

		public ActionResult BritishKittens()
		{
			var v = from pet in DbStorage.Instance.Pets where pet.BreedID == 3 && !pet.IsParent select pet;
			return View(v.ToList());
		}

		public ActionResult MainKunKittens()
		{
			var v = from pet in DbStorage.Instance.Pets where pet.BreedID == 1 && !pet.IsParent select pet;
			return View(v.ToList());
		}
	}
}