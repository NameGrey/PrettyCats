using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCats.Helpers
{
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