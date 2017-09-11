using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DM.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
               name: "Detail",
               url: "{controller}/{action}/{IdClass}",
               defaults: new { controller = "Home", action = "Detail", IdClass = UrlParameter.Optional }
           );
        }
    }
}
