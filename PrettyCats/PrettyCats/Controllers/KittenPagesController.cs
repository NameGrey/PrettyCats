﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PrettyCats.Database;

namespace PrettyCats.Controllers
{
	public class KittenPagesController : Controller
	{
		public ActionResult KittenMainPage(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult ParentCatMainPage()
		{
			return View();
		}

		public ActionResult GetKittenHtml(int id)
		{
			return View(DbStorage.Instance.Pets.Find(id));
		}

		public ActionResult AllAvailableKittens()
		{
			return View();
		}
	}
}