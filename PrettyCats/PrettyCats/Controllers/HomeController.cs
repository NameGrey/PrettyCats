using System.Linq;
using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
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

		// GET: Home
		public ActionResult Index()
		{
			var v = DbStorage.Pets.ToList();

			return View(v);
		}

		public ActionResult Error()
		{
			return View();
		}

		[Route("contacts")]
		public ActionResult ContactsPage()
		{
			return View();
		}
	}
}