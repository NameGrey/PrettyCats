﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrettyCats.Database;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : Controller
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

		public ActionResult KittenMainPage(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult ParentCatMainPage(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult KittenOnTheMainPageHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult AllAvailableKittens()
		{
			return View();
		}

		#region Display kittens

		public ActionResult AllParents()
		{
			var v = DbStorage.Instance.Pets.Where(i=>i.IsParent).ToList();
			return View(v);
		}

		public ActionResult BengalKittens()
		{
			var v = DbStorage.GetKittensByBreed(2);
			return View(v.ToList());
		}

		public ActionResult BritishKittens()
		{
			var v = DbStorage.GetKittensByBreed(3).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4));
			return View(v.ToList());
		}

		public ActionResult MainKunKittens()
		{
			var v = DbStorage.GetKittensByBreed(1);
			return View(v.ToList());
		}

		public ActionResult Archive()
		{
			return View();
		}

		public ActionResult BengalKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(2, true);
			return View("BengalKittens", v.ToList());
		}

		public ActionResult BritishKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(3, true).ToList();
			v.AddRange(DbStorage.GetKittensByBreed(4, true));

			return View("BritishKittens", v.ToList());
		}

		public ActionResult MainKunKittens_Archive()
		{
			var v = DbStorage.GetKittensByBreed(1, true);
			return View("MainKunKittens", v.ToList());
		}
		#endregion
	}
}