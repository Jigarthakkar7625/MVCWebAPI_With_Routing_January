using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MVCWebAPI_January.Auth
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // Approve the client authentication
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //var db1 = new DB_January_BatchEntities(); //Despose
            //db1.Dispose();

            using (var db = new DB_January_BatchEntities())
            {

                var checkUser = db.Customers.Where(x => x.Name == context.UserName && x.City == context.Password).FirstOrDefault();

                if (checkUser != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("CustomerId", checkUser.CustomerID.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Email, checkUser.Name));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("Invalid", "Invalid username and password");
                    context.Rejected();
                }
            }

            //User and password >> validate

        }
    }
}