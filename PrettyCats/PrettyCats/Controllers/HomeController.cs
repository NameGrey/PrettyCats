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

		public ActionResult ContactsPage()
		{
			return View();
		}

		public ActionResult ThemePage()
		{
			return View();
		}

		public ActionResult AllAvailableKittens()
		{
			return View();
		}
	}
}