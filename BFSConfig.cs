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
			get { return (LoggingConаigElement)this["logging"]; }
			set { this["logging"] = value; }
		}

		public class LoggingConаigElement : ConfigurationElement
		{
			[ConfigurationProperty("traceEnabled", DefaultValue = true, IsRequired = false)]
			public bool TraceEnabled
			{
				get { return (bool)this["traceEnabled"]; }
				set { this["traceEnabled"] = value; }
			}
		}
	}
}