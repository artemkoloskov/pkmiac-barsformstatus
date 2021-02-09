using System.Configuration;

namespace PKMIAC.BARSFormStatus
{
	public class BFSConfig : ConfigurationSection
	{
		public BFSConfig()
		{
		}

		[ConfigurationProperty("logging", IsRequired = false)]
		public LoggingConаigElement Logging
		{
			get => (LoggingConаigElement)this["logging"];
			set => this["logging"] = value;
		}

		public class LoggingConаigElement : ConfigurationElement
		{
			[ConfigurationProperty("traceEnabled", DefaultValue = false, IsRequired = false)]
			public bool TraceEnabled
			{
				get => (bool)this["traceEnabled"];
				set => this["traceEnabled"] = value;
			}

			[ConfigurationProperty("basicLoggerEnabled", DefaultValue = false, IsRequired = false)]
			public bool BasicLoggerEnabled
			{
				get => (bool)this["basicLoggerEnabled"];
				set => this["basicLoggerEnabled"] = value;
			}
		}
	}
}