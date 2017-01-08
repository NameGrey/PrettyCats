using System;
using System.Diagnostics;
using NLog;

namespace PrettyCats.Helpers
{
	public static class LogHelper
	{
		public const string StartAppFormatMessage = "Application is starting...";
		public const string PageNotFoundFormatMessage = "Page not found - {0}";
		public const string GlobalExceptionFormatMessage = "Global exception!";

		#region ImageWorker

		public const string AddPhotoFormatMessage = "Add photo: " +
		                                            "kittenNameNumbered='{0}'," +
		                                            "kittenNameNumberedSmall='{1}'," +
		                                            "dirPath='{2}'" +
		                                            ",linkPath='{3}'," +
		                                            "smallLinkPath='{4}'";

		public const string SaveMainPictureStartFormatMessage = "Save Main picture for {0}";
		public const string SaveMainPictureInfFormatMessage = "Path = {0}";
		public const string PictureRemovedFormatMessage = "Picture removed id = {0}";
		public const string FileRemovedFormatMessage = "File removed Path = {0}";

		#endregion
	}
}