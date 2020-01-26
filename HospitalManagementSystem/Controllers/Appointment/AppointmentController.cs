using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Appointment;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Appointment
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AppointmentController : ApiController
    {
        [HttpPost, Route("api/appointment/AddAppointment")]
        public async Task<IHttpActionResult> AddAppointment(AppointmentModel appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
                var count = await AppointmentService.GetAppointmentCount(appointment);
                if(count > 15)
                {
                    return BadRequest("Appointments Full!!");
                }
                else
                {
                    if (await AppointmentService.AddAppointment(appointment))
                    {
                        return Ok("Appointment Added Successfully!");
                    }
                    else
                    {
                        return BadRequest("Appointment Adding Failed!");
                    }
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpGet, Route("api/appointment/GetAppointments")]
        public async Task<IHttpActionResult> GetAppointments()
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
                var appointments = await AppointmentService.GetAppointments();
                if (appointments.Count > 0)
                {
                    return Ok(appointments);
                }
                else
                {
                    return BadRequest("No Appointments Found!");
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpGet, Route("api/appointment/GetFees")]
        public async Task<IHttpActionResult> GetFees()
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
                var fees = await AppointmentService.GetFees();
                if (fees.Count > 0)
                {
                    return Ok(fees);
                }
                else
                {
                    return BadRequest("No Appointments Found!");
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [HttpPost, Route("api/appointment/UpdateAppointment")]
        public async Task<IHttpActionResult> UpdateAppointment(AppointmentModel appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
                if (await AppointmentService.AppointmentExists(appointment))
                {
                    if (await AppointmentService.UpdateAppointment(appointment))
                    {
                        return Ok("Appointment Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Appointment!");
                    }
                }
                else
                {
                    return BadRequest("No Such Appointment Exisits!");
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }


        [HttpPost, Route("api/appointment/DeleteAppointment")]
        public async Task<IHttpActionResult> DeleteAppointment(AppointmentModel appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
                if (await AppointmentService.AppointmentExists(appointment))
                {
                    if (await AppointmentService.DeleteAppointment(appointment))
                    {
                        return Ok("Appointment Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Appointment!");
                    }
                }
                else
                {
                    return BadRequest("No Such Appointment Exisits!");
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }
    }
}
