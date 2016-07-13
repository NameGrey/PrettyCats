using System.Web.Http.ExceptionHandling;

namespace PrettyCats.Services
{
	public class GlobalExceptionsHandler:ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context)
		{
			//TODO: Add logging of exceptions
			base.Handle(context);
		}
	}
}