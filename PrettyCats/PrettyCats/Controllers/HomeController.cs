using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using PrettyCats.Database;

namespace PrettyCats.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult KittenOnTheMainPageHtml()
		{
			return View();
		}

		public ActionResult ContactsPage()
		{
			return View();
		}
	}
}