using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;
using FirstAPI.WebService.Utilities;
using System.Web.Http.Dispatcher;
using FirstAPI.WebService.Custom;

namespace FirstAPI.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //  //Part: 35
            //  config.Routes.MapHttpRoute(
            //     name: "Version1",
            //     routeTemplate: "api/v1/employees/{id}",
            //     defaults: new { id = RouteParameter.Optional, controller = "EmployeeV1" }
            // );

            //  config.Routes.MapHttpRoute(
            //   name: "Version2",
            //   routeTemplate: "api/v2/employees/{id}",
            //   defaults: new { id = RouteParameter.Optional, controller = "EmployeeV2" }
            //);


            ////Part: 36.2
            //config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));

            ////Part: 39.2
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.kadertech.employee.v1+json"));
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.kadertech.employee.v2+json"));

            //config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.kadertech.employee.v1+xml"));
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.kadertech.employee.v2+xml"));


            ////Part: 14.1        //Install-package WebApiContrib.Formatting.Jsonp
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //Part: 15.1         //Install-package Microsoft.AspNet.WebApi.Cors
            //origins, headers, methods
            //"http://localhost:60694,http://kader.com", "*", "GET,POST"
            EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);
            //config.EnableCors();

            //Part: 17.2
            //config.Filters.Add(new RequireHttpsAttribute());

        }
    }
}
