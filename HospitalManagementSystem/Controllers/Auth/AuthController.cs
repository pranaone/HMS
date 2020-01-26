using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.NetworkInformation;
using System.Net;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Models.User;
using System.Web.Http.Cors;
using HospitalManagementSystem.Models.Common;

namespace HospitalManagementSystem.Controllers.Auth
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        /// <summary>
        /// API End point to register a new user
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <returns></returns>
        [HttpPost, Route("api/auth/Register")]
        public async Task<IHttpActionResult> Register(UserModel userForRegisterDto)
        {
            if (await AuthService.UserExists(userForRegisterDto.Email, userForRegisterDto.ID))
            {
                return BadRequest("Email Already Exists");
            }

            var usertocreate = new UserModel
            {
                Firstname = userForRegisterDto.Firstname,
                Lastname = userForRegisterDto.Lastname,
                PostalCode = userForRegisterDto.PostalCode,
                Email = userForRegisterDto.Email,
                Gender = userForRegisterDto.Gender,
                RegisteredDate = DateTime.Now,
                MobileNo = userForRegisterDto.MobileNo,
                RoleID = userForRegisterDto.RoleID,
                Doctor_Category = userForRegisterDto.Doctor_Category
            };

            var isRegistrationComplete = await AuthService.Register(usertocreate, userForRegisterDto.Password);

            if (isRegistrationComplete.IsError)
            {
                return BadRequest("Registration Not Successful!");
            }
            else
            {
                return Ok("Registration Successful!");
            }

        }


        [HttpPost, Route("api/auth/Login")]
        public async Task<IHttpActionResult> Login(UserModel userForLoginDto)
        {
            //sms.Sendsms();

            var userFromRepo = await AuthService.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            else
            {
                var claimsData = new[] {
                        new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString()),
                        new Claim(ClaimTypes.Name, userFromRepo.Email),
                        //new Claim(ClaimTypes.Role, userFromRepo.RoleID.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key for creating the token"));

                var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claimsData),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = signingCreds

                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                Ping ping = new Ping();
                var replay = ping.Send(Dns.GetHostName());
                IPAddress userIP = null;
                if (replay.Status == IPStatus.Success)
                {
                    userIP = replay.Address;
                }

                OperatingSystem os_info = System.Environment.OSVersion;

                LoggedInUserDTO loggedInUser = new LoggedInUserDTO()
                {
                    ID = userFromRepo.ID,
                    Firstname = userFromRepo.Firstname,
                    Lastname = userFromRepo.Lastname,
                    Email = userFromRepo.Email,
                    Gender = userFromRepo.Gender,
                    RoleID = userFromRepo.RoleID,
                    RegisteredDate = userFromRepo.RegisteredDate,
                    Token = tokenHandler.WriteToken(token),
                    UserLoggedInTimezone = TimeZone.CurrentTimeZone.ToString(),
                    UserLoginDate = DateTime.Now.ToString(),
                    UserLoggedInIP = userIP.ToString(),
                    UserLoginOs = os_info.VersionString,
                    ActiveStatus = userFromRepo.ActiveStatus
                };

                CommonResponse response = AuthService.UserLoginDetailsAdded(loggedInUser);

                return Ok(loggedInUser);

            }
        }

        /// <summary>
        /// API End point to 
        /// </summary>
        /// <param name="userLogoutDto"></param>
        /// <returns></returns>
        [HttpPost, Route("api/auth/Logout")]
        public async Task<IHttpActionResult> Logout(LoggedInUserDTO userLogoutDto)
        {
            var validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                LoggedInUserDTO loggedOutUserDTO = new LoggedInUserDTO()
                {
                    UserLoggedOutDate = DateTime.Now.ToString(),
                    ID = userLogoutDto.ID,
                    Email = userLogoutDto.Email
                };

                var response = AuthService.UserLoginDetailsUpdate(loggedOutUserDTO);

                if (!response.IsError)
                {
                    return Ok("Logged Out Successfully!");
                }
                else
                {
                    return BadRequest("Logout Not Successful!");
                }
            }
            else if(validatedResponse.TokenHasExpired)
            {
                return BadRequest("Token Expired!");
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}