using System.Web.Http.ExceptionHandling;
using NLog;

namespace PrettyCats.Services
{
	public class GlobalExceptionsHandler:ExceptionHandler
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public override void Handle(ExceptionHandlerContext context)
		{
			_logger.Error(context.Exception, context.Request.RequestUri.AbsolutePath);
			base.Handle(context);
		}
	}
}