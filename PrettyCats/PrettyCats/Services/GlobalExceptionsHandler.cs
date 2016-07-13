using System.Web.Http.ExceptionHandling;
using log4net;
using log4net.Config;

namespace PrettyCats.Services
{
	public class GlobalExceptionsHandler:ExceptionHandler
	{
		private readonly ILog _logger = log4net.LogManager.GetLogger(typeof (GlobalExceptionsHandler));

		public override void Handle(ExceptionHandlerContext context)
		{
			_logger.Error(context.Request.RequestUri, context.Exception);
			base.Handle(context);
		}
	}
}