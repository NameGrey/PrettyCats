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
		public ActionResult KittenMainPage(int id)
		{
			return View(DbStorage.Pets.Find(i => i.ID == id));
		}

		[Route("parent-kitten-page/{id}")]
		public ActionResult ParentCatMainPage(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		public ActionResult KittenOnTheMainPageHtml(int id)
		{
			return View(DbStorage.Pets.Find(i=>i.ID == id));
		}

		[Route("kittens")]
		public ActionResult AllAvailableKittens()
		{
			return View();
		}

		#region Display kittens
		[Route("parent-kittens")]
		public ActionResult AllParents()
		{
			var v = DbStorage.Pets.Where(i => i.IsParent && i.WhereDisplay != 3).ToList();
			return View(v);
		}

		[Route("bengal-kittens")]
		public ActionResult BengalKittens()
		{
			var v = DbStorage.GetKittensByBreed(2);
			return View(v.ToList());
		}

		[Route("scotland-kittens")]
		public ActionResult BritishKittens()
		{
			var v = DbStorage.GetKittensByBreed(3).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4));
			return View(v.ToList());
		}

		[Route("mainkun-kittens")]
		public ActionResult MainKunKittens()
		{
			var v = DbStorage.GetKittensByBreed(1);
			return View(v.ToList());
		}

		[Route("archive")]
		public ActionResult Archive()
		{
			return View();
		}

		[Route("bengal-kittens-archive")]
		public ActionResult BengalKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(2, true);
			return View("BengalKittens", v.ToList());
		}

		[Route("scotland-kittens-archive")]
		public ActionResult BritishKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(3, true).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4, true));

			return View("BritishKittens", v.ToList());
		}

		[Route("mainkun-kittens-archive")]
		public ActionResult MainKunKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(1, true);
			return View("MainKunKittens", v.ToList());
		}
		#endregion
	}
}