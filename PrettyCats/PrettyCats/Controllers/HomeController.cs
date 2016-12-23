using System.Web.Mvc;
using PrettyCats.DAL;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

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