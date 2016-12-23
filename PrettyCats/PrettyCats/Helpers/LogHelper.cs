using System;

namespace PrettyCats.Helpers
{
	//TODO: change logging system - use NLog instead of direct writing into file system
	//TODO: and database using log4net. NLog is faster and logs async way. It doesn't affect
	//TODO: perfomance of main application. It's needed to create a configuration inside application (not in conf file)
	public static class LogHelper
	{
		public static void WriteLog(string filename, string message)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine("=======================" + DateTime.Now.ToString() + "=======================");
			builder.AppendLine(message);
			builder.AppendLine("=================================================");

			System.IO.File.AppendAllText(filename, builder.ToString());
		}
	}
}