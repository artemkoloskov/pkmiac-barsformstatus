using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace PKMIAC.BARSFormStatus
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Конфигурация и службы веб-API
			BFSConfig bfsConfig = (BFSConfig)ConfigurationManager.GetSection("bfsConfigs");

			if (bfsConfig.Logging.TraceEnabled)
			{
				SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
				traceWriter.IsVerbose = true;
				traceWriter.MinimumLevel = TraceLevel.Debug;

				System.Diagnostics.Trace.Listeners.Clear();
				System.Diagnostics.Trace.Listeners.Add(
				   new System.Diagnostics.TextWriterTraceListener("C:\\BARSFormStatus\\trace.txt")); 
			}

			//Конфигурация службы для возврата данных в формате JSON
			config.Formatters.Add(new BrowserJsonFormatter());

			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
				= ReferenceLoopHandling.Ignore;

			// Маршруты веб-API
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}

	public class BrowserJsonFormatter : JsonMediaTypeFormatter
	{
		public BrowserJsonFormatter()
		{
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			SerializerSettings.Formatting = Formatting.Indented;

			SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		}

		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			base.SetDefaultContentHeaders(type, headers, mediaType);

			headers.ContentType = new MediaTypeHeaderValue("application/json");
		}
	}
}
