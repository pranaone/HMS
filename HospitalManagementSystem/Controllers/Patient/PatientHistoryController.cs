using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Patient;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Patient
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PatientHistoryController : ApiController
    {
        [HttpPost, Route("api/patient/AddPatientHistory")]
        public async Task<IHttpActionResult> AddPatientHistory(PatientHistoryModel patientHistory)
        {
            if (patientHistory == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            if (await PatientHistoryService.AddPatientHistory(patientHistory))
            {
                return Ok("Patient History Successfully Saved!");
            }
            else
            {
                return BadRequest("Fail to Save Patient History!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpPost, Route("api/patient/GetPatientHistory")]
        public async Task<IHttpActionResult> GetPatientHistory()
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patientHIstories = await PatientHistoryService.GetPatientHistory();
            if (patientHIstories.Count > 0)
            {
                return Ok(patientHIstories);
            }
            else
            {
                return BadRequest("No History Data Found!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpPost, Route("api/patient/SearchPatientHistory")]
        public async Task<IHttpActionResult> SearchPatientHistory(PatientHistoryModel patientHistory)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patientsHistories = await PatientHistoryService.SearchPatientHistory(patientHistory);
            if (patientsHistories.Count > 0)
            {
                return Ok(patientsHistories);
            }
            else
            {
                return BadRequest("No Such Patient Exists!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpPost, Route("api/patient/UpdatePatientHistory")]
        public async Task<IHttpActionResult> UpdatePatientHistory(PatientHistoryModel patientHistory)
        {
            if (patientHistory == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            {

                if (await PatientHistoryService.UpdatePatientHistory(patientHistory))
                {
                    return Ok("Patient History Updated Successfully!");
                }
                else
                {
                    return BadRequest("Failed to Update Patient History!");
                }
                //}
                //else
                //{
                //    return Unauthorized();
                //}
            }
        }

    }
}
