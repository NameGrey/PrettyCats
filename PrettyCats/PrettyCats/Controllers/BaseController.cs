using System.Web.Mvc;
using NLog;
using PrettyCats.Helpers;

namespace PrettyCats.Controllers
{
	public class BaseController: Controller
	{
		private Logger _logger = LogManager.GetCurrentClassLogger();

		protected override void OnException(ExceptionContext filterContext)
		{
			_logger.Fatal(filterContext.Exception, "Unexpected controller error!");

			if (filterContext.HttpContext.IsCustomErrorEnabled)
			{
				filterContext.ExceptionHandled = true;
				this.View("Error").ExecuteResult(this.ControllerContext);
			}
		}
	}
}