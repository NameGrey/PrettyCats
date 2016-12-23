using System.Linq;
using System.Web.Mvc;
using PrettyCats.DAL;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		private readonly IKittensRepository _kittensRepository;

		public HomeController()
		{
			_kittensRepository = new DBKittensRepository(new StorageContext());
		}

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
			return View(_kittensRepository.GetCollection().ToList());
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