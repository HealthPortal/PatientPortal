using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HealthPortal.Models;
using System.Web.Routing;

namespace HealthPortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "api/{controller}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            //     );
            //config.Routes.MapHttpRoute(
            //name: "actionapi",
            //routeTemplate: "api/{controller}/{action}/{id}",
            //defaults: new { id= RouteParameter.Optional}
            //    );

            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
             ).RouteHandler = new SessionStateRouteHandler();
            //).RouteHandler = new SessionStateRouteHandler();

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
