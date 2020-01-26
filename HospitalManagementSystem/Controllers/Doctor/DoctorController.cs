using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Doctor;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Doctor
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DoctorController : ApiController
    {
        //[HttpPost, Route("api/doctor/AddDoctor")]
        //public async Task<IHttpActionResult> AddDoctor(DoctorModel doctor)
        //{
        //    if (doctor == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
        //    //if (!validatedResponse.IsError)
        //    //{
        //    if (await DoctorService.DoctorExists(doctor))
        //    {
        //        return BadRequest("Doctor Already Exists");
        //    }
        //    else
        //    {
        //        if (await DoctorService.AddDoctor(doctor))
        //        {
        //            return Ok("Patient Doctor Successfully!");
        //        }
        //        else
        //        {
        //            return BadRequest("Doctor Adding Failed!");
        //        }
        //    }
        //    //}
        //    //else
        //    //{
        //    //    return Unauthorized();
        //    //}
        //}

        [HttpGet, Route("api/doctor/GetDoctors")]
        public async Task<IHttpActionResult> GetDoctors()
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var doctors = await DoctorService.GetDoctors();
            if (doctors.Count > 0)
            {
                return Ok(doctors);
            }
            else
            {
                return BadRequest("No Doctors Exists!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }


        [HttpGet, Route("api/doctor/SearchDoctors")]
        public async Task<IHttpActionResult> SearchDoctor(DoctorModel doctor)
        {
            //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            //if (!validatedResponse.IsError)
            //{
            var doctors = await DoctorService.SearchDoctors(doctor);
            if (doctors.Count > 0)
            {
                return Ok(doctors);
            }
            else
            {
                return BadRequest("No Doctors Exists!");
            }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        //[HttpPost, Route("api/doctor/UpdateDoctor")]
        //public async Task<IHttpActionResult> UpdateDoctor(DoctorModel doctor)
        //{
        //    if (doctor == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
        //    //if (!validatedResponse.IsError)
        //    //{
        //    if (await DoctorService.DoctorExists(doctor))
        //    {
        //        if (await DoctorService.UpdateDoctor(doctor))
        //        {
        //            return Ok("Doctor Updated Successfully!");
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to Update Doctor!");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("No Such Doctor Exisits!");
        //    }
        //    //}
        //    //else
        //    //{
        //    //    return Unauthorized();
        //    //}
        //}


        //[HttpPost, Route("api/doctor/DeleteDoctor")]
        //public async Task<IHttpActionResult> DeleteDoctor(DoctorModel doctor)
        //{
        //    if (doctor == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    //CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
        //    //if (!validatedResponse.IsError)
        //    //{
        //    if (await DoctorService.DoctorExists(doctor))
        //    {
        //        if (await DoctorService.DeleteDoctor(doctor))
        //        {
        //            return Ok("Doctor Deleted Successfully!");
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to Delete Doctor!");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("No Such Doctor Exisits!");
        //    }
        //    //}
        //    //else
        //    //{
        //    //    return Unauthorized();
        //    //}
        //}
    }
}
