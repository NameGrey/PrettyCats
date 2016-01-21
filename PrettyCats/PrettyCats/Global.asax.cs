﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PrettyCats.Controllers;

namespace PrettyCats
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			
			log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Application_EndRequest()
		{
			if (Context.Response.StatusCode == 404)
			{
				Response.Clear();

				var rd = new RouteData();
				rd.Values["controller"] = "Home";
				rd.Values["action"] = "Index";

				IController c = new HomeController();
				c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
			}
		}
	}
}
