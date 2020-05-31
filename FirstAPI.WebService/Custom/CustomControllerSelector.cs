using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace FirstAPI.WebService.Custom
{
    //Part: 36.1
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();       //Get only all api Controller
            var routeData = request.GetRouteData();         //Get request Url or Route(Convention based or Attribute route) data
            var controllerName = routeData.Values["controller"].ToString(); // Get Controller Name from request Url or Route

            
            string versionNumber = "1";

            ////  /api/employee?v=1
            //var versionQuerystring = HttpUtility.ParseQueryString(request.RequestUri.Query); //take Url/Route QueryString (?v=1)
            //if (versionQuerystring["v"] != null)
            //{
            //    versionNumber = versionQuerystring["v"];
            //}

            ////Part:37   X-EmployeeService-Vertion: 1
            //string customHeader = "X-EmployeeService-Vertion";
            //if (request.Headers.Contains(customHeader))
            //{
            //    versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();

            //    if (versionNumber.Contains(","))
            //    {
            //        versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
            //    }
            //}

            ////Part:38.1   Accept: application/json; version=1
            //var acceptHeader = request.Headers.Accept.Where(a => a.Parameters.Count(p => p.Name.ToLower() == "vertion") > 0);
            //if (acceptHeader.Any())
            //{
            //    versionNumber = acceptHeader.First().Parameters.First(p => p.Name.ToLower() == "vertion").Value;
            //}

                                                                      //v1+xml
            //Part: 39.1     Accept: application/vnd.kadertech.employee.v1+json
            var regex = @"application\/vnd\.kadertech\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";

            var acceptHeader = request.Headers.Accept.Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));
            if (acceptHeader.Any())
            {
                var match = Regex.Match(acceptHeader.First().MediaType, regex, RegexOptions.IgnoreCase);
                versionNumber = match.Groups["version"].Value;
            }

            if (versionNumber == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }


            HttpControllerDescriptor controllerDescriptor;
            if(controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            //return base.SelectController(request);
            return null;
        }

    }
}