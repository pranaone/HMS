using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Pharmacy;


namespace HospitalManagementSystem.Controllers.Pharmacy
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MedicineController : ApiController
    {
        [HttpPost, Route("api/Pharmacy/AddMedicine")]
        public async Task<IHttpActionResult> AddMedicine(MedicineModel medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await MedicineService.MedicineExist(medicine))
                {
                    return BadRequest("Medicine Already Exists");
                }
                else
                {
                    if (await MedicineService.AddMedicine(medicine))
                    {
                        return Ok("Medicine Added Successfully!");
                    }
                    else
                    {
                        return BadRequest("Medicine Adding Failed!");
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of add

        [HttpGet, Route("api/Pharmacy/GetMedicine")]
        public async Task<IHttpActionResult> GetMedicine(MedicineModel medicine)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var Medicine = await MedicineService.GetMedicine(medicine);
                if (Medicine.Count > 0)
                {
                    return Ok(Medicine);
                }
                else
                {
                    return BadRequest("No Medicine record Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/UpdateMedicine")]
        public async Task<IHttpActionResult> UpdateMedicine(MedicineModel medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await MedicineService.MedicineExist(medicine))
                {
                    if (await MedicineService.UpdateMedicine(medicine))
                    {
                        return Ok("Medicine Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Medicine!");
                    }
                }
                else
                {
                    return BadRequest("No Such Medicine Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of update


        [HttpPost, Route("api/Pharmacy/DeleteMedicine")]
        public async Task<IHttpActionResult> DeleteMedicine(MedicineModel medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await MedicineService.MedicineExist(medicine))
                {
                    if (await MedicineService.DeleteMedicine(medicine))
                    {
                        return Ok("Medicine Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Medicine!");
                    }
                }
                else
                {
                    return BadRequest("No Such Medicine Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of Delete

    }
}