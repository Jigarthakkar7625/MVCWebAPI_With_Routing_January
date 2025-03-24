using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web.Http;

namespace MVCWebAPI_January.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("GetToken")]
        public IHttpActionResult GetToken()
        {

            var getToken = GenerateToken();
            return Ok(getToken);

            //var getList = _dbContext.EmpAddresses.ToList();
            //return Ok(getList); // 200 OK

        }

        public object GenerateToken()
        {
            string key = "E9DB7E89123F52A9F2DB04EF04C7FE88";
            var issuer = "https://localhost:44356/";

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            using (var db = new DB_January_BatchEntities())
            {

                var checkUser = db.Customers.Where(x => x.Name == "HElllooo" && x.City == "fdsfkhskdsfh").FirstOrDefault();

                if (checkUser != null)
                {

                    var listClim = new List<Claim>();
                    listClim.Add(new Claim("CustomerId", checkUser.CustomerID.ToString()));
                    listClim.Add(new Claim(ClaimTypes.Role, "Admin"));
                    listClim.Add(new Claim(ClaimTypes.Email, checkUser.Name));

                    var token = new JwtSecurityToken(issuer,
                        issuer,
                        listClim,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credential
                        );

                    var generatetoken = new JwtSecurityTokenHandler().WriteToken(token);

                    return new { data = generatetoken };
                }
            }
            return new { data = "" };
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            // save
            return new string[] { "value1", "value2" };
        }

        // RESTFUL
        // REST

        [HttpPost]
        [Route("Get123")]
        // GET api/values/5
        public string Get123(int a)
        {
            return "value";
        }


        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
