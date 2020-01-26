using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.User;
using HospitalManagementSystem.Services.DoctorCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.DoctorCategory
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DoctorCategoryController : ApiController
    {
        [HttpPost, Route("api/doctorCategory/AddDoctorCategory")]
        public async Task<IHttpActionResult> AddDoctorCategory(DoctorCategoryModel doctorCategory)
        {
            if (await DoctorCategoryService.DoctorCategoryExists(doctorCategory.Name))
            {
                return BadRequest("Doctor Category Already Exists");
            }
            else
            {
                var doctorCategorytocreate = new DoctorCategoryModel
                {
                    Name = doctorCategory.Name
                };

                CommonResponse response = await DoctorCategoryService.AddNewDoctorCategory(doctorCategorytocreate);

                if (response.IsError)
                {
                    return BadRequest("Error In Adding The New Doctor Category!");
                }
                else
                {
                    return Ok("Doctor Category Added Successfully!");
                }
            }
        }

        [HttpGet, Route("api/doctorCategory/GetDoctorCategories")]
        public async Task<IHttpActionResult> GetDoctorCategories()
        {
            var response = await DoctorCategoryService.GetDoctorCategories();

            if (response == null)
            {
                return BadRequest("Error In Getting Doctor Categories!");
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPost, Route("api/doctorCategory/UpdateDoctorCategory")]
        public async Task<IHttpActionResult> UpdateDoctorCategory(DoctorCategoryModel doctorCategory)
        {
            if (await DoctorCategoryService.DoctorCategoryExistsForUpdateAndDelete(doctorCategory.ID))
            {
                var doctorCategorytocreate = new DoctorCategoryModel
                {
                    ID = doctorCategory.ID,
                    Name = doctorCategory.Name
                };

                CommonResponse response = await DoctorCategoryService.UpdateDoctorCategory(doctorCategorytocreate);

                if (response.IsError)
                {
                    return BadRequest("Error In Updating The Doctor Category!");
                }
                else
                {
                    return Ok("Doctor Category Updated Successfully!");
                }
            }
            else
            {
                return BadRequest("Doctor Category Already Exists");
            }
        }

        [HttpPost, Route("api/doctorCategory/DeleteDoctorCategory")]
        public async Task<IHttpActionResult> DeleteDoctorCategory(DoctorCategoryModel doctorCategory)
        {
            if (await DoctorCategoryService.DoctorCategoryExistsForUpdateAndDelete(doctorCategory.ID))
            {
                var doctorCategorytoDelete = new DoctorCategoryModel
                {
                    ID = doctorCategory.ID,
                    Name = doctorCategory.Name
                };

                CommonResponse roleResponse = await DoctorCategoryService.DeleteDoctorCategory(doctorCategorytoDelete);

                if (roleResponse.IsError)
                {
                    return BadRequest("Error In Deleting The Doctor Category!");
                }
                else
                {
                    return Ok("Doctor Category Deleting Successfully!");
                }
            }
            else
            {
                return BadRequest("Doctor Category Does Not Exist!!");
            }
        }


    }
}