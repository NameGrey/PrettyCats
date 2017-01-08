using System;
using System.Diagnostics;
using NLog;

namespace PrettyCats.Helpers
{
	public sealed class LogHelper
	{
		private static readonly object _lock = new object();
		private static volatile LogHelper _instance;
		private static Logger _logger;

		private LogHelper()
		{
			
		}

		public static LogHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
						{
							_instance = new LogHelper();
							Debug.Write("Create logger");
							_logger = LogManager.GetLogger("AppLogger");
						}
					}
				}
				return _instance;
			}
		}

		#region Logging methods

		public void WriteInfo(string message)
		{
			_logger.Info(message);
		}

		public void WriteError(string message)
		{
			_logger.Error(message);
		}

		public void WriteFatalError(string message)
		{
			_logger.Fatal(message);
		}

		public void WriteFatalError(string message, Exception exception)
		{
			_logger.Fatal(exception, message);
		}

		#endregion
	}
}