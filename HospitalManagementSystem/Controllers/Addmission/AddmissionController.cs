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
    public class AddmissionController : ApiController
    {//

        [HttpPost, Route("api/addmission/AdmitPatient")]
        public async Task<IHttpActionResult> AdmitPatient(InPatientModel addmission)
        {
            if (addmission == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await AddmissionService.AddInPatient(addmission))
            {
                return Ok("Patient Addmission Successfull!");
            }
            else
            {
                return BadRequest("Addmission Failed!");
            }
        }

        //[HttpGet, Route("api/addmission/GetWardDetails")]
        //public async Task<IHttpActionResult> GetWardDetails()
        //{

        //    var addmissions = await AddmissionService.GetWardDetails();
        //    if (addmissions.Count > 0)
        //    {
        //        return Ok(addmissions);
        //    }
        //    else
        //    {
        //        return BadRequest("No Details Found!");
        //    }     

        //}

        [HttpPost, Route("api/addmission/UpdateDisDateRoomStatus")]
        public async Task<IHttpActionResult> UpdateDisDateRoomStatus(DischargeBillModel discharge)
        {
            if (discharge == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await AddmissionService.UpdateDisDateRoomStatus(discharge))
            {
                return Ok("Date and Status Updated Successfully!");
            }
            else
            {
                return BadRequest("Failed to Update Details!");
            }
        }//end of update


    }
}
