using System.Web.Mvc;

namespace PrettyCats.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Error()
		{
			return View();
		}

		[Route("contacts")]
		public ActionResult Contacts()
		{
			return View("ContactsPage");
		}
	}
}