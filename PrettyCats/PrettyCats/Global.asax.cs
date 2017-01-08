using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using NLog;
using PrettyCats.Controllers;
using PrettyCats.Helpers;

namespace PrettyCats
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		protected void Application_Start()
		{
			_logger.Info(LogHelper.StartAppFormatMessage);
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			UnityConfig.RegisterComponents();

			GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			//TODO: it's not needed for Production enviroment
			if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
			{
				HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
				HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
				HttpContext.Current.Response.End();
			}
		}

		protected void Application_EndRequest()
		{
			if (Context.Response.StatusCode == 404)
			{
				_logger.Error(LogHelper.PageNotFoundFormatMessage, Request.Url);
				Response.Clear();

				var rd = new RouteData();
				rd.Values["controller"] = "Home";
				rd.Values["action"] = "Index";

				IController c = new HomeController();
				c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
			}
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			_logger.Fatal(exception, LogHelper.GlobalExceptionFormatMessage);
			Server.ClearError();
			Response.Redirect("/Home/Error");
		}
	}
}
