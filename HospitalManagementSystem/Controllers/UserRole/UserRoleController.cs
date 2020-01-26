using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.User;
using HospitalManagementSystem.Services.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.UserRole
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserRoleController : ApiController
    {
        [HttpPost, Route("api/userRole/AddUserRole")]
        public async Task<IHttpActionResult> AddUserRole(UserRoleModel userRole)
        {
            if (await UserRoleService.UserRoleExists(userRole.Name))
            {
                return BadRequest("User Role Already Exists");
            }
            else
            {
                var usertocreate = new UserRoleModel
                {
                    Name = userRole.Name
                };

                CommonResponse roleResponse = await UserRoleService.AddNewUserRole(usertocreate);

                if (roleResponse.IsError)
                {
                    return BadRequest("Error In Adding The New Uer Role!");
                }
                else
                {
                    return Ok("Successfully Added A New User Role!");
                }
            }
            
        }

        [HttpGet, Route("api/userRole/GetUserRoles")]
        public async Task<IHttpActionResult> GetUserRoles()
        {
            var rolesResponse = await UserRoleService.GetUserRoles();

            if (rolesResponse == null)
            {
                return BadRequest("Error In Getting User Roles!");
            }
            else
            {
                return Ok(rolesResponse);
            }
        }


        [HttpPost, Route("api/userRole/UpdateUserRole")]
        public async Task<IHttpActionResult> UpdateUserRole(UserRoleModel userRole)
        {
            if (await UserRoleService.UserRoleExistsForUpdateAndDelete(userRole.ID))
            {
                var usertocreate = new UserRoleModel
                {
                    ID = userRole.ID,
                    Name = userRole.Name
                };

                CommonResponse roleResponse = await UserRoleService.UpdateRole(usertocreate);

                if (roleResponse.IsError)
                {
                    return BadRequest("Error In Updating The User Role!");
                }
                else
                {
                    return Ok("User Role Updated Successfully!");
                }
            }
            else
            {
                return BadRequest("User Role Does Not Exist!");
            }
        }

        [HttpPost, Route("api/userRole/DeleteUserRole")]
        public async Task<IHttpActionResult> DeleteUserRole(UserRoleModel userRole)
        {
            if (await UserRoleService.UserRoleExistsForUpdateAndDelete(userRole.ID))
            {
                var userRoleToDelete = new UserRoleModel
                {
                    ID = userRole.ID,
                    Name = userRole.Name
                };

                CommonResponse roleResponse = await UserRoleService.DeleteRole(userRoleToDelete);

                if (roleResponse.IsError)
                {
                    return BadRequest("Error In Deleting The User Role!");
                }
                else
                {
                    return Ok("User Role Deleted Successfully!");
                }
            }
            else
            {
                return BadRequest("User Role Does Not Exist!");
            }
        }

    }
}