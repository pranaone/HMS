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
    public class PatientController : ApiController
    {
        [HttpPost, Route("api/patient/AddPatient")]
        public async Task<IHttpActionResult> AddPatient(PatientModel patient)
        {
            if (patient == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await PatientService.PatientExists(patient))
                {
                    return BadRequest("Patient Already Exists");
                }
                else
                {
                    if (await PatientService.AddPatient(patient))
                    {
                        return Ok("Patient Added Successfully!");
                    }
                    else
                    {
                        return BadRequest("Patient Adding Failed!");
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet, Route("api/patient/GetPatients")]
        public async Task<IHttpActionResult> GetPatients()
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patients = await PatientService.GetPatients();
            if (patients.Count > 0)
            {
                return Ok(patients);
            }
            else
            {
                return BadRequest("No Patients Exists!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpGet, Route("api/patient/GetPatientsForDashboard")]
        public async Task<IHttpActionResult> GetPatientsForDashboard()
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var patients = await PatientService.GetPatientsForDashboard();
                if (patients != null)
                {
                    return Ok(patients);
                }
                else
                {
                    return BadRequest("No Patients Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost, Route("api/patient/SearchPatient")]
        public async Task<IHttpActionResult> SearchPatients(PatientModel patient)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patients = await PatientService.SearchPatient(patient);
            if (patients.Count > 0)
            {
                return Ok(patients);
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


        [HttpPost, Route("api/patient/SearchPatientForAdmission")]
        public async Task<IHttpActionResult> SearchPatientsForAdmission(PatientAdmissionModel patient)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patients = await PatientService.SearchPatientForAdmission(patient);
            if (patients.Count > 0)
            {
                return Ok(patients);
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

        [HttpPost, Route("api/patient/SearchPatientNIC")]
        public async Task<IHttpActionResult> SearchPatientNIC(PatientModel patient)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var patients = await PatientService.SearchPatientNIC(patient);
            if (patients.Count > 0)
            {
                return Ok(patients);
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

        [HttpPost, Route("api/patient/UpdatePatient")]
        public async Task<IHttpActionResult> UpdatePatient(PatientModel patient)
        {
            if (patient == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await PatientService.PatientExists(patient))
                {
                    if (await PatientService.UpdatePatient(patient))
                    {
                        return Ok("Patient Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Patient!");
                    }
                }
                else
                {
                    return BadRequest("No Such Patient Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("api/patient/DeletePatient")]
        public async Task<IHttpActionResult> DeletePatient(PatientModel patient)
        {
            if (patient == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await PatientService.PatientExists(patient))
                {
                    if (await PatientService.DeletePatient(patient))
                    {
                        return Ok("Patient Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Patient!");
                    }
                }
                else
                {
                    return BadRequest("No Such Patient Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
