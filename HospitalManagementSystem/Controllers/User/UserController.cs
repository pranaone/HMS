using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.User;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        [HttpGet, Route("api/user/GetUsersForUsersPage")]
        public async Task<IHttpActionResult>GetUsersForUsersPage()
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var rolesResponse = await UserService.GetUsersForUsersPage();

                if (rolesResponse.Count == 0)
                {
                    return BadRequest("Error In Getting User Roles!");
                }
                else
                {
                    return Ok(rolesResponse);
                }
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpPost, Route("api/user/ChangeUserActiveStatus")]
        public async Task<IHttpActionResult> ChangeUserActiveStatus(UserForUsersPage userUpdateDTO)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var usertoUpdate = new UserForUsersPage
                {
                    ID = userUpdateDTO.ID,
                    ActiveStatus = userUpdateDTO.ActiveStatus
                };

                CommonResponse roleResponse = await UserService.UpdateUserActiveStatus(usertoUpdate);

                if (roleResponse.IsError)
                {
                    return BadRequest("Error In Updating The User Active Status!");
                }
                else
                {
                    return Ok("User Active Status Updated Successfully!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("api/user/GetUserForMyAccount")]
        public async Task<IHttpActionResult> GetUserForMyAccount(UserForUsersPage userUpdateDTO)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var rolesResponse = await UserService.GetUserForMyAccount(userUpdateDTO);

                if (rolesResponse == null)
                {
                    return BadRequest("Error In Getting User Roles!");
                }
                else
                {
                    return Ok(rolesResponse);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}