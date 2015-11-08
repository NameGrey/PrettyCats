using System.Web.Mvc;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult KittenMainPage()
		{
			return View();
		}

		public ActionResult ParentCatMainPage()
		{
			return View();
		}
	}
}