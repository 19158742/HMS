using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HotelApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
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

            config.Routes.MapHttpRoute(
                  name: "BookmyroomApi",
                  routeTemplate: "{controller}/{action}",
                  defaults: new { controller = "Bookmyroom", action = "Post" }
);
        }
    }
}
