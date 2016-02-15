using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class InformPageController : Controller
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

		// GET: InformPages

		[Route("kak-vibrat-kotenka")]
		public ActionResult HowChooseKitten()
		{
			Pages page = new Pages();
			page.Content = "sd";
			page.Name = "Как выбрать котенка?";

			return View("InformPage", page);
		}

		[Route("kitten-cost-price")]
		public ActionResult KittenCostArticle()
		{
			Pages page = new Pages();
			page.Content = "sd";
			page.Name = "Сколько стоят породистые котята?";

			return View("KittenCostArticle", page);
		}

		public ActionResult Documents()
		{
			return View();
		}

		[Route("articles")]
		public ActionResult Articles()
		{
			return View();
		}

		public ActionResult Archive()
		{
			return View();
		}
	}
}