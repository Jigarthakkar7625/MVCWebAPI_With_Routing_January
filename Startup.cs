using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MVCWebAPI_January.Auth;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MVCWebAPI_January.Startup))]

namespace MVCWebAPI_January
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                AllowInsecureHttp = true,
                Provider = new OAuthProvider()
            };

            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
