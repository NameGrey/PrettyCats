using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace BookIt
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();
			config.EnableCors();

			config.Routes.MapHttpRoute(
				  name: "Api_Get",
				  routeTemplate: "api/{controller}/{action}/{id}",
				  defaults: new { id = RouteParameter.Optional, action = "Get" },
				  constraints: new { httpMethod = new HttpMethodConstraint("GET") }
			   );

			config.Routes.MapHttpRoute(
				   name: "Api_Post",
				   routeTemplate: "api/{controller}/{action}/{id}",
				   defaults: new { id = RouteParameter.Optional, action = "Post" },
				   constraints: new { httpMethod = new HttpMethodConstraint("POST") }
				);

			config.Routes.MapHttpRoute(
				  name: "Api_Delete",
				  routeTemplate: "api/{controller}/{action}/{id}",
				  defaults: new { id = RouteParameter.Optional, action = "Delete" },
				  constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
			   );

		}

	}
}
