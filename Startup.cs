using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MVCWebAPI_January.Auth;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(MVCWebAPI_January.Startup))]

namespace MVCWebAPI_January
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseJwtBearerAuthentication(
                new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("E9DB7E89123F52A9F2DB04EF04C7FE88")),
                        ValidIssuer = "https://localhost:44356/",
                        ValidAudience = "https://localhost:44356/",

                    }
                }
                );


            //OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
            //    AllowInsecureHttp = true,
            //    Provider = new OAuthProvider()
            //};

            //app.UseOAuthAuthorizationServer(option);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
