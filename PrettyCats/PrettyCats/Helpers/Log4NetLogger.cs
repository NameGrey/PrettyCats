using System;
using log4net;
using log4net.Core;

namespace PrettyCats.Helpers
{
	public class Log4NetLogger : ILogger
	{

		private ILog _logger;

		public Log4NetLogger()
		{
			_logger = LogManager.GetLogger(this.GetType());
		}

		public void Info(string message)
		{
			_logger.Info(message);
		}

		public void Warn(string message)
		{
			_logger.Warn(message);
		}

		public void Debug(string message)
		{
			_logger.Debug(message);
		}

		public void Error(string message)
		{
			_logger.Error(message);
		}

		public void Error(Exception x)
		{
			Error(String.Format("Message: {0}; StackTrace: {1}; ", x.Message, x.StackTrace));
		}

		public void Error(string message, Exception x)
		{
			_logger.Error(message, x);
		}

		public void Fatal(string message)
		{
			_logger.Fatal(message);
		}

		public void Fatal(Exception x)
		{
			Fatal(String.Format("Message: {0}; StackTrace: {1}; ", x.Message, x.StackTrace));
		}

		public bool IsEnabledFor(Level level)
		{
			return _logger.Logger.IsEnabledFor(level);
		}

		public void Log(LoggingEvent logEvent)
		{
			_logger.Logger.Log(logEvent);
		}

		public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
		{
			_logger.Logger.Log(callerStackBoundaryDeclaringType, level,message, exception);
		}

		public string Name
		{
			get { return "Log4NetDbLogger"; }
		}

		public log4net.Repository.ILoggerRepository Repository
		{
			get { return _logger.Logger.Repository; }
		}
	}
}