﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using Owin;
using FirstAPI.WebService.Providers;
using FirstAPI.WebService.Models;
using FirstAPI.Data;
using FirstAPI.WebService.Facebook;

namespace FirstAPI.WebService
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),      //Part: 23
                //AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(10),

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //Part:29.2
            var facebookOptions = new FacebookAuthenticationOptions()
            {
                AppId= "2686320351694726",
                AppSecret = "1b8a85e4877b5e43c26e8b679f44759a",
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,email"
            };
            facebookOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookOptions);

            //Part:28.1
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "467432482088-9n4kb8s750du23h4fpskdlolg5ajr6qs.apps.googleusercontent.com",
                ClientSecret = "K_MwDUBTUlpdVCfCSUYHhjxy"
            });
        }
    }
}
