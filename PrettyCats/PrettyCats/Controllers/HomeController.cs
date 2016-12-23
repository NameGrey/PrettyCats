using System.Web.Mvc;
using PrettyCats.DAL;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IKittensRepository _kittensRepository;

		public HomeController()
		{
			_kittensRepository = new DBKittensRepository(new StorageContext());
		}

		// GET: Home
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