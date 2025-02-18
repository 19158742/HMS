﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HM_WebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.EnableCors();
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			//config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{action}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{controller}/{action}/{id}",
				defaults: new { controller = "Invoice", action = "GetItems", id = RouteParameter.Optional }

			);
		}
	}
}
