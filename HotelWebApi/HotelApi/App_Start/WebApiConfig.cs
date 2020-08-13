using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using HotelApi.Models;

namespace HotelApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Filters.Add(new BasicAuthenticationAttribute());
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //  name: "InvoiceRoute",
            //  routeTemplate: "{controller}/{action}/{id}",
            //  defaults: new { controller = "Invoice", action = "GetItems", id = RouteParameter.Optional }

            //);
            config.Routes.MapHttpRoute(
                  name: "RoomApi",
                  routeTemplate: "{controller}/{action}/{id}",
                  defaults: new { controller = "Room", action = "Get", id = RouteParameter.Optional }
            );

//            config.Routes.MapHttpRoute(
//                  name: "BookmyroomApi",
//                  routeTemplate: "{controller}/{action}",
//                  defaults: new { controller = "Bookmyroom", action = "Post" }
//);
        }
    }
}
