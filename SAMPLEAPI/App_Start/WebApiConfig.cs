using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace SAMPLEAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ePortalDataSourceApi",
				routeTemplate: "ep/{controller}/{action}"
			);

			config.Routes.MapHttpRoute(name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
               
                defaults: new { id = RouteParameter.Optional, controller = "Employee", action = "Search", }
                
 
            );
        }
    }
}
