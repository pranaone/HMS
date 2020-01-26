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
//this enables (cross platform resource sharing) external requests to the controller
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TreatmentController : ApiController
    {
        [HttpPost, Route("api/addmission/AddTreatment")]
        public async Task<IHttpActionResult> AddTreatment(TreatmentModel treatment)
        {
            if (treatment == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await TreatmentService.AddTreatment(treatment))
            {
                return Ok("Treatment Saved Successfully!");
            }
            else
            {
                return BadRequest("Failed to Save Treatment!");
            }
        }

        [HttpPost, Route("api/addmission/SearchAdmission")]
        public async Task<IHttpActionResult> SearchAdmission(AdmissionTreatmentModel Admission)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var admission = await TreatmentService.SearchAdmission(Admission);
            if (admission != null)
            {
                return Ok(admission);
            }
            else
            {
                return BadRequest("Admission Number doesnt Exists!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }//end of search

        [HttpPost, Route("api/addmission/UpdateTreatment")]
        public async Task<IHttpActionResult> UpdateTreatment(TreatmentModel treatment)
        {
            if (treatment == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await TreatmentService.UpdateTreatment(treatment))
            {
                return Ok("Treatment Udpated Successfully!");
            }
            else
            {
                return BadRequest("Failed to Update Ward!");
            }
        }//end of update

        [HttpPost, Route("api/addmission/GetAdmissionTreatment")]
        public async Task<IHttpActionResult> GetAdmissionTreatment(PatientTreatmentModel treatment)
        {

            var Treatments = await TreatmentService.GetAdmissionTreatment(treatment);
            if (Treatments!=null)
            {
                return Ok(Treatments);
            }
            else
            {
                return BadRequest("No Treatments Available!");
            }

        }
    }
}
