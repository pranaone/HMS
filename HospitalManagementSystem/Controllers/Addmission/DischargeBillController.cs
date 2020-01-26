using HospitalManagementSystem.Models.Addmission;
using HospitalManagementSystem.Services.Addmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Addmission
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DischargeBillController : ApiController
    {
        [HttpPost, Route("api/addmission/AddDisBill")]
        public async Task<IHttpActionResult> AddDisBill(DischargeBillModel dischargebill)
        {
            if (dischargebill == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await DIschargeBillService.AddDischargeBill(dischargebill))
            {
                return Ok("Patient Discharged Successfully!");
            }
            else
            {
                return BadRequest("Failed to Save Discharge Details!");
            }
        }
    }
}
