using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BookIt
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (HttpContext.Current.Request.HttpMethod == "OPTIONS")//нужно для Chrome, иначе HTTPDelete в хроме не работает
			{
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
				HttpContext.Current.Response.End();
			}
		}
	}
}
