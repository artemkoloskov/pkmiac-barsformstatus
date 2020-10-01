using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PKMIAC.BARSFormStatus
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Конфигурация и службы веб-API

			//Конфигурация службы для возврата данных в формате JSON
			config.Formatters.Add(new BrowserJsonFormatter());

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
