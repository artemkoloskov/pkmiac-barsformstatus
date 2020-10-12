using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace PKMIAC.BARSFormStatus.Data
{
	public class BFSLoggerProvider : ILoggerProvider
	{
		public ILogger CreateLogger(string categoryName)
		{
			return new BFSLogger();
		}

		public void Dispose() { }

		private class BFSLogger : ILogger
		{
			public IDisposable BeginScope<TState>(TState state)
			{
				return null;
			}

			public bool IsEnabled(LogLevel logLevel)
			{
				return true;
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId,
					TState state, Exception exception, Func<TState, Exception, string> formatter)
			{
				File.AppendAllText("C:\\BARSFormStatus\\log.txt", formatter(state, exception));
				Console.WriteLine(formatter(state, exception));
			}
		}
	}
}