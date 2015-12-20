using System.Linq;
using System.Web.Mvc;
using PrettyCats.Database;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var v = DbStorage.Instance.Pets.ToList();

			return View(v);
		}

		public ActionResult ContactsPage()
		{
			return View();
		}
	}
}