using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.User;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.User
{
    public class UserService : BaseAppTenant
    {
        public static async Task<List<UserForUsersPage>> GetUsersForUsersPage()
        {
            List<UserForUsersPage> AllUsers = new List<UserForUsersPage>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isExistingUserQuery = "SELECT * from User_Role";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand("SP_GetUsersForUsersPage", dbConn);
                    SqlCommand cmd = new SqlCommand(isExistingUserQuery, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserForUsersPage user = new UserForUsersPage();
                            user.ID = reader.GetInt32(0);
                            user.Firstname = reader.GetString(1);
                            user.Lastname = reader.GetString(2);
                            user.Address_line_1 = reader.GetString(3);
                            user.Address_line_2 = reader.GetString(4);
                            user.PostalCode = reader.GetString(5);
                            user.Email = reader.GetString(6);
                            user.Gender = reader.GetString(9);
                            user.MobileNo = reader.GetString(10);
                            user.RoleID = reader.GetString(11);
                            user.Doctor_Category = reader.GetString(12);
                            user.RegisteredDate = reader.GetDateTime(13);
                            user.ActiveStatus = reader.GetString(14);
                            AllUsers.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AllUsers = null;
                }
                finally
                {
                    dbConn.Close();
                }

                return AllUsers;
            }
        }


        public static async Task<List<UserForUsersPage>> GetUserForMyAccount(UserForUsersPage currentUser)
        {
            List<UserForUsersPage> AllUsers = new List<UserForUsersPage>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetSingleUser", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", currentUser.ID);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserForUsersPage user = new UserForUsersPage();
                            user.ID = reader.GetInt32(0);
                            user.Firstname = reader.GetString(1);
                            user.Lastname = reader.GetString(2);
                            user.Address_line_1 = reader.GetString(3);
                            user.Address_line_2 = reader.GetString(4);
                            user.PostalCode = reader.GetString(5);
                            user.Email = reader.GetString(6);
                            user.Gender = reader.GetString(9);
                            user.MobileNo = reader.GetString(10);
                            user.RoleID = reader.GetString(11);
                            user.Doctor_Category = reader.GetString(12);
                            user.RegisteredDate = reader.GetDateTime(13);
                            user.ActiveStatus = reader.GetString(14);
                            AllUsers.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AllUsers = null;
                }
                finally
                {
                    dbConn.Close();
                }

                return AllUsers;
            }
        }


        public static async Task<CommonResponse> UpdateUserActiveStatus(UserForUsersPage user)
        {
            CommonResponse response = new CommonResponse();
            //String SQL = "UPDATE User_Role SET Name = '" + role.Name + "' WHERE ID = '" + role.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    bool active = false;
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    if(user.ActiveStatus == "Active")
                    {
                        active = false;
                    }
                    else
                    {
                        active = true;
                    }

                    SqlCommand cmd = new SqlCommand("SP_UpdateUserActiveStatus", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActiveStatus", active);
                    cmd.Parameters.AddWithValue("@RoleID", user.ID);

                    await cmd.ExecuteNonQueryAsync();
                    dbConn.Close();

                    response.IsError = false;
                }
                catch (Exception ex)
                {
                    response.IsError = true;
                }
                finally
                {
                    dbConn.Close();
                }
            }

            return response;
        }


    }
}