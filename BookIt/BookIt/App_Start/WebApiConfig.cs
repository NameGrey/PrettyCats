using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace BookIt
{
	//http://www.toptal.com/angular-js/a-step-by-step-guide-to-your-first-angularjs-app
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "ActionApi_Get",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
                  name: "ActionApi_Post",
                  routeTemplate: "api/{controller}/{action}/{id}",
                  defaults: new { id = RouteParameter.Optional, action = "Post" },
                  constraints: new { httpMethod = new HttpMethodConstraint("POST") });

        }

    }
}
