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
    public class WardController : ApiController
    {
        [HttpPost, Route("api/addmission/AddWard")]
        public async Task<IHttpActionResult> AddWard(WardModel ward)
        {
            if (ward == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await WardService.AddWard(ward))
            {
                return Ok("Ward Saved Successfully!");
            }
            else
            {
                return BadRequest("Failed to Save Ward!");
            }
        }

        [HttpPost, Route("api/addmission/UpdateWard")]
        public async Task<IHttpActionResult> UpdateWard(WardModel ward)
        {
            if (ward == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await WardService.UpdateWard(ward))
            {
                return Ok("Ward Udpated Successfully!");
            }
            else
            {
                return BadRequest("Failed to Update Ward!");
            }
        }

        //[HttpPost, Route("api/addmission/DeleteWard")]
        //public async Task<IHttpActionResult> DeleteWard(WardModel ward)
        //{
        //    if (ward == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    if (await WardService.DeleteWard(ward))
        //    {
        //        return Ok("Ward Deleted Successfully!");
        //    }
        //    else
        //    {
        //        return BadRequest("Failed to Delete Ward!");
        //    }
        //}


        [HttpPost, Route("api/addmission/GetWardDoc")]
        public async Task<IHttpActionResult> GetWardDoc(WardModel ward)
        {

            var wards = await WardService.GetDocByWardID(ward);
            if (wards.Count > 0)
            {   
                return Ok(wards);
            }
            else
            {
                return BadRequest("No Doctor Available!");
            }

        }


        [HttpGet, Route("api/addmission/GetWards")]
        public async Task<IHttpActionResult> GetWards()
        {

            var ward = await WardService.GetWards();
            if (ward.Count > 0)
            {
                return Ok(ward);
            }
            else
            {
                return BadRequest("No Wards Found!");
            }
        }

        [HttpGet, Route("api/addmission/GetAvailableDoc")]
        public async Task<IHttpActionResult> GetAvailableDoc()
        {

            var ward = await WardService.GetAvailableDoc();
            if (ward.Count > 0)
            {
                return Ok(ward);
            }
            else
            {
                return BadRequest("No Wards Found!");
            }
        }

        //[HttpPost, Route("api/addmission/GetWardDoctor")]
        //public async Task<IHttpActionResult> GetWardDoctor(WardModel ward)
        //{

        //    var docs = await WardService.GetWardDoctor(ward);
        //    if (docs.Count > 0)
        //    {
        //        return Ok(docs);
        //    }
        //    else
        //    {
        //        return BadRequest("No Wards Found!");
        //    }

        //}

    }

}

