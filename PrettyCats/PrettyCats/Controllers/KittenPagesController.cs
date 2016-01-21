using System;
using System.Linq;
using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : Controller
	{
		protected override void OnException(ExceptionContext filterContext)
		{
			LogHelper.WriteLog(Server.MapPath("~/App_Data/" + Settings.LogFileName), filterContext.Exception.ToString());

			if (filterContext.HttpContext.IsCustomErrorEnabled)
			{
				filterContext.ExceptionHandled = true;
				this.View("Error").ExecuteResult(this.ControllerContext);
			}
		}

		[Route("kitten-page/{id}")]
		public ActionResult KittenMainPage_old(int id)
		{
			return View("KittenMainPage", DbStorage.Pets.Find(i => i.ID == id));
		}

		[Route("parent-kitten-page/{id}")]
		public ActionResult ParentCatMainPage_old(int id)
		{
			return View("ParentCatMainPage", DbStorage.Pets.Find(i=>i.ID == id));
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		public ActionResult KittenOnTheMainPageHtml(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		#region Display kittens

		[Route("kittens")]
		public ActionResult AllAvailableKittens_old()
		{
			return View("AllAvailableKittens");
		}

		[Route("parent-kittens")]
		public ActionResult AllParents_old()
		{
			var v = DbStorage.Pets.Where(i => i.IsParent && i.WhereDisplay != 3).ToList();
			return View("AllParents", v);
		}

		[Route("bengal-kittens")]
		public ActionResult BengalKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(2);
			return View("BengalKittens", v.ToList());
		}

		[Route("scotland-kittens")]
		public ActionResult BritishKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(3).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4));
			return View("BritishKittens", v.ToList());
		}

		[Route("mainkun-kittens")]
		public ActionResult MainKunKittens_old()
		{
			var v = DbStorage.GetKittensByBreed(1);
			return View("MainKunKittens", v.ToList());
		}

		[Route("archive")]
		public ActionResult Archive_old()
		{
			return View("Archive");
		}

		[Route("bengal-kittens-archive")]
		public ActionResult BengalKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(2, true);
			return View("BengalKittens", v.ToList());
		}

		[Route("scotland-kittens-archive")]
		public ActionResult BritishKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(3, true).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4, true));

			return View("BritishKittens", v.ToList());
		}

		[Route("mainkun-kittens-archive")]
		public ActionResult MainKunKittens_Archive_old()
		{
			var v = DbStorage.GetKittensByBreed(1, true);
			return View("MainKunKittens", v.ToList());
		}
		#endregion

		//These pages should be removed when search system find new urls for them
		#region Redirect pages
		
		public ActionResult KittenMainPage(int id)
		{
			return RedirectPermanent(String.Format("/kitten-page/{0}", id));
		}

		public ActionResult ParentCatMainPage(int id)
		{
			return RedirectPermanent(String.Format("/parent-kitten-page/{0}", id));
		}

		public ActionResult AllAvailableKittens()
		{
			return RedirectPermanent("/kittens");
		}
		
		public ActionResult AllParents()
		{
			return RedirectPermanent("/parent-kittens");
		}
		
		public ActionResult BengalKittens()
		{
			return RedirectPermanent("/bengal-kittens");
		}
		
		public ActionResult BritishKittens()
		{
			return RedirectPermanent("/scotland-kittens");
		}

		public ActionResult MainKunKittens()
		{
			return RedirectPermanent("/mainkun-kittens");
		}

		public ActionResult Archive()
		{
			return RedirectPermanent("/archive");
		}

		public ActionResult BengalKittens_Archive()
		{
			return RedirectPermanent("/bengal-kittens-archive");
		}

		public ActionResult BritishKittens_Archive()
		{
			return RedirectPermanent("/scotland-kittens-archive");
		}

		public ActionResult MainKunKittens_Archive()
		{
			return RedirectPermanent("/mainkun-kittens-archive");
		}
		#endregion
	}
}