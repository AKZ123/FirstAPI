using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirstAPI.WebService.Utilities
{
    //override for always send https
    //Part: 17.1    //Use HTTPS instead of HTTP
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps) //Request Https na holay
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);   //catch Request
                actionContext.Response.Content = new StringContent("<p>Use HTTPS instead of HTTP</p>", Encoding.UTF8, "text/html");    //and add some html with Response

                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                uriBuilder.Scheme = Uri.UriSchemeHttps;      //make HTTPS uri
                uriBuilder.Port = 44379;        //https://localhost:44379/

                actionContext.Response.Headers.Location = uriBuilder.Uri;   //send new created HTTPS Uri to Response
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}