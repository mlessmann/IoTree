using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IoTree.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "tree/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}