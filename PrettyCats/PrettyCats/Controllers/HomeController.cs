using System.Linq;
using System.Web.Mvc;
using PrettyCats.DAL.Repositories;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		private IKittensRepository kittensRepository;

		public HomeController()
		{
			kittensRepository = new DBKittensRepository(new StorageContext());
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
			return View(kittensRepository.GetCollection().ToList());
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

		//These pages should be removed when search system find new urls for them
		#region Redirect pages

		public ActionResult ContactsPage()
		{
			return RedirectPermanent("/contacts");
		}

		#endregion
	}
}