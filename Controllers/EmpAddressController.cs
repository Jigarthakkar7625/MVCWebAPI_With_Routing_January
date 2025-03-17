using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCWebAPI_January.Controllers
{
    // Routing => Conv, attribute
    [RoutePrefix("api/EmpAddress")]
    public class EmpAddressController : ApiController
    {
        private readonly DB_January_BatchEntities _dbContext = new DB_January_BatchEntities();

        [HttpGet]
        [Route("GetEmpAddresses")]
        public IHttpActionResult GetEmpAddresses()
        {
            var getList = _dbContext.EmpAddresses.ToList();
            return Ok(getList); // 200 OK

        }

        [HttpGet]
        [Route("GetEmpAddress/{id}/{userid}")] // Attribute routing
        public IHttpActionResult GetEmpAddress(int id, int userid)
        {
            var getEmpAddress = _dbContext.EmpAddresses.Where(x => x.EmployeeAddressID == id).FirstOrDefault();
            return Ok(getEmpAddress); // 200 OK

        }

        [HttpPost]
        [Route("SaveEmpAddress")]
        public IHttpActionResult SaveEmpAddress(EmpAddress empAddress)
        {
            _dbContext.EmpAddresses.Add(empAddress);
            _dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "EmpAddress", id = empAddress.EmployeeAddressID }, empAddress);


        }


        //200 >> OK 
        //201 >> Created
        //204 >> no content
        //400 >> Bad request
        //404 >> Not found



        [HttpPut]
        [Route("UpdateEmpAddress")]
        public IHttpActionResult UpdateEmpAddress(int employeeAddressID, EmpAddress empAddress)
        {

            if (empAddress == null || string.IsNullOrEmpty(empAddress.Address))
            {
                return BadRequest("Data is not valid");
            }

            var getEmpAddress = _dbContext.EmpAddresses.Where(x => x.EmployeeAddressID == employeeAddressID).FirstOrDefault();

            if (getEmpAddress == null)
            {
                return NotFound();
            }

            getEmpAddress.Address = empAddress.Address;
            getEmpAddress.EmployeeID = empAddress.EmployeeID;
            _dbContext.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }


        [HttpDelete]
        [Route("DeleteEmpAddress")]
        public IHttpActionResult DeleteEmpAddress(int employeeAddressID)
        {

            var getEmpAddress = _dbContext.EmpAddresses.Where(x => x.EmployeeAddressID == employeeAddressID).FirstOrDefault();
            _dbContext.EmpAddresses.Remove(getEmpAddress);

            _dbContext.SaveChanges();

            return StatusCode(HttpStatusCode.Gone);


        }


    }
}
