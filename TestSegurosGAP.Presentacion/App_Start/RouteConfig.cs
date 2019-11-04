﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestSegurosGAP.Presentacion.App_Start;

namespace TestSegurosGAP.Presentacion
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new
                {
                    serverRoute = new ServerRouteConstraint(url =>
                    {
                        return url.PathAndQuery.StartsWith("/Settings", StringComparison.InvariantCultureIgnoreCase);
                    })
                }
            );

            routes.MapRoute(name: "angular", url: "{*url}", defaults: new { controller = "Home", action = "Index" });
        }
    }
}
