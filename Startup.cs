using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BirdiSysAssignment.App_Start;

[assembly: OwinStartup(typeof(BirdiSysAssignment.Startup))]

namespace BirdiSysAssignment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable OAuth Token Generation
            OAuthAuthorizationServerOptions oAuthOptions = new OAuthAuthorizationServerOptions
            {

                TokenEndpointPath = new PathString("/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                AllowInsecureHttp = true 
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Web API configuration
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}