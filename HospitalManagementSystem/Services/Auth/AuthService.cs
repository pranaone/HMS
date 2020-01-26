using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.User;
using HospitalManagementSystem.Services.BaseService;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Auth
{
    public class AuthService : BaseAppTenant
    {
        public static async Task<bool> UserExists(string username, int userID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingUserQuery = "SELECT * from [User] WHERE email ='" + username + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand(isExistingUserQuery, dbConn);

                    // 1.  create a command object identifying the stored procedure
                    SqlCommand cmd = new SqlCommand("isExistingUserQuery", dbConn);

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@Email", username));

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    return false;
                }
                finally
                {
                    dbConn.Close();
                }

            }

        }

        public static async Task<CommonResponse> Register(UserModel user, string password)
        {
            var response = new CommonResponse();
            string passwordHash, passwordSalt;

            GeneratePasswordHash(password, out passwordHash, out passwordSalt);

            user.Password = passwordHash;
            user.Salt = passwordSalt;

            try
            {
                using (SqlConnection dbConn = new SqlConnection(connectionString))
                {
                    dbConn.Open();
                    String SQL = GetUserInsertQuery(user);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.ExecuteNonQuery();

                    dbConn.Close();
                }


                response.IsError = false;

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = "Error in Registering User!";
            }

            return response;

        }

        private static void GeneratePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var salt = new byte[256];
            rng.GetBytes(salt);
            passwordSalt = Convert.ToBase64String(salt);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
            System.Security.Cryptography.SHA256Managed sha256hashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashString.ComputeHash(bytes);

            passwordHash = Convert.ToBase64String(hash);
        }

        private static string GetUserInsertQuery(UserModel user)
        {
            return "INSERT INTO [User](Firstname,Lastname,Address_line_1,Address_line_2,PostalCode,Email," +
                    "Gender,MobileNo,Password,Salt,RoleID,Doctor_Category,RegisteredDate,ActiveStatus) " +
                    "VALUES('" + user.Firstname + "','" +
                                    user.Lastname + "','" +
                                    user.Address_line_1 + "','" +
                                    user.Address_line_2 + "','" +
                                    user.PostalCode + "','" +
                                    user.Email + "','" +
                                    user.Gender + "','" +
                                    user.MobileNo + "','" +
                                    user.Password + "','" +
                                    user.Salt + "','" +
                                    user.RoleID + "','" +
                                    user.Doctor_Category + "','" +
                                    user.RegisteredDate + "','" +
                                    user.ActiveStatus + "')";
        }

        public static async Task<UserModel> Login(string email, string password)
        {
            UserModel logginInUser = new UserModel();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isExistingUserQuery = "SELECT * from [User] WHERE Email ='" + email + "' AND ActiveStatus = 1";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isExistingUserQuery, dbConn);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            logginInUser.ID = reader.GetInt32(0);
                            logginInUser.Firstname = reader.GetString(1);
                            logginInUser.Lastname = reader.GetString(2);
                            logginInUser.Address_line_1 = reader.GetString(3);
                            logginInUser.Address_line_2 = reader.GetString(4);
                            logginInUser.PostalCode = reader.GetString(5);
                            logginInUser.Email = reader.GetString(6);
                            logginInUser.Password = reader.GetString(7);
                            logginInUser.Salt = reader.GetString(8);
                            logginInUser.Gender = reader.GetString(9);
                            logginInUser.MobileNo = reader.GetString(10);
                            logginInUser.RoleID = reader.GetInt32(11);
                            logginInUser.Doctor_Category = reader.GetInt32(12);
                            logginInUser.RegisteredDate = reader.GetDateTime(13);
                            logginInUser.ActiveStatus = reader.GetInt32(14);
                        }
                    }
                    else
                    {
                        logginInUser = null;
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    logginInUser = null;
                }
                finally
                {
                    dbConn.Close();
                }

            }

            if (!VerifyPasswordHash(password, logginInUser.Password, logginInUser.Salt))
            {
                return null;
            }

            return logginInUser;
        }

        private static bool VerifyPasswordHash(string password, string user_passwordHash, string user_password_salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + user_password_salt);
            System.Security.Cryptography.SHA256Managed sha256hashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashString.ComputeHash(bytes);

            var enteredPasswordHas = Convert.ToBase64String(hash);

            if (enteredPasswordHas != user_passwordHash)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<CommonResponse> ValidateUserAndToken()
        {
            HttpContext httpContext = HttpContext.Current;
            string headerToken = httpContext.Request.Headers["Authorization"];

            CommonResponse response = new CommonResponse();

            if (headerToken == null)
            {
                response.IsError = true;
            }
            else
            {
                var name = GetName(headerToken);
                var userID = int.Parse(GetUserIdFromToken(headerToken));

                if (checkIfTokenHasExpired(headerToken))
                {
                    var responseDelToken = DeleteTokenInfo(headerToken, userID, name);
                    if (!responseDelToken.IsError)
                    {
                        response.IsError = true;
                        response.TokenHasExpired = true;
                    }
                }
                else
                {
                    if (AuthorizeUser(headerToken, userID))
                    {
                        response.IsError = false;
                    }
                    else
                    {
                        response.IsError = true;
                    }
                }
            }

            return response;
        }

        public static bool checkIfTokenHasExpired(string tokenString)
        {
            bool hasExpired = false;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(tokenString) as JwtSecurityToken;
            var tokenExpiryDate = token.ValidTo;

            // If there is no valid `exp` claim then `ValidTo` returns DateTime.MinValue
            if (tokenExpiryDate == DateTime.MinValue)
            {
                hasExpired = true;
                //throw new Exception("Could not get exp claim from token");
            }

            // If the token is in the past then you can't use it
            if (tokenExpiryDate < DateTime.UtcNow)
            {
                hasExpired = true;
                //throw new Exception($"Token expired on: {tokenExpiryDate}");
            }

            return hasExpired;

        }

        public static bool AuthorizeUser(string token, int userID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isExistingUserQuery = "SELECT * from User_Login WHERE Token = '" + token + "' AND userID ='" + userID + "' AND user_logged_out_date IS NOT NULL ";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isExistingUserQuery, dbConn);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    return false;
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }

        public static CommonResponse DeleteTokenInfo(string token, int userID, string email)
        {
            var response = new CommonResponse();
            String SQL = "DELETE FROM User_Login WHERE Token = '" + token + "' AND UserID = '" + userID + "'";
            try
            {
                using (SqlConnection dbConn = new SqlConnection(connectionString))
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.ExecuteNonQuery();

                    dbConn.Close();
                }

            }
            catch (Exception)
            {
                response.IsError = true;
                response.Message = "Error in Deleting the Logout Details For User!";
            }

            return response;
        }

        public static string GetName(string token)
        {
            string secret = "super secret key for creating the token";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }

        public static string GetUserIdFromToken(string token)
        {
            string secret = "super secret key for creating the token";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static CommonResponse UserLoginDetailsAdded(LoggedInUserDTO user)
        {
            var response = new CommonResponse();
            String SQL = "INSERT INTO User_Login(userID," +
                                                "user_role," +
                                                "user_login_os," +
                                                "user_login_date," +
                                                "user_logged_in_timezone," +
                                                "user_logged_in_IP," +
                                                "user_logged_out_date," +
                                                "token)" +
                        "VALUES('" + user.ID + "','"
                                     + user.RoleID + "','"
                                     + user.UserLoginOs + "', GETDATE() ,'"
                                     + user.UserLoggedInTimezone + "','"
                                     + user.UserLoggedInIP + "','"
                                     + user.UserLoggedOutDate + "','"
                                     + user.Token
                                     + "')";
            try
            {
                using (SqlConnection dbConn = new SqlConnection(connectionString))
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.ExecuteNonQuery();

                    dbConn.Close();
                }

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = "Error in Registering User!";
            }

            return response;
        }

        public static CommonResponse UserLoginDetailsUpdate(LoggedInUserDTO user)
        {
            var response = new CommonResponse();
            String SQL = "UPDATE User_Login " +
                            "SET UserLoggedOutDate= GETDATE() WHERE Token = '" + user.Token + "' AND UserID = '" + user.ID + "'";
            try
            {
                using (SqlConnection dbConn = new SqlConnection(connectionString))
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.ExecuteNonQuery();

                    dbConn.Close();
                }

            }
            catch (Exception)
            {
                response.IsError = true;
                response.Message = "Error in Updating the Logout Details For User!";
            }

            return response;
        }

    }
}