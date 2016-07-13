using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.WebSockets;
using PrettyCats.Services;

namespace PrettyCats
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Services.Replace(typeof (IExceptionHandler), new GlobalExceptionsHandler());

			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}");
		}
	}
}