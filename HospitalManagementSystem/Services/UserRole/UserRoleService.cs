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

namespace HospitalManagementSystem.Services.UserRole
{
    public class UserRoleService : BaseAppTenant
    {
        public static async Task<bool> UserRoleExists(string userRole)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingUserQuery = "SELECT * from UserRole WHERE Name ='" + userRole + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_IsExistingUserRole", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRole", userRole);

                    reader = await cmd.ExecuteReaderAsync();
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

        public static async Task<bool> UserRoleExistsForUpdateAndDelete(int userRoleID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingUserQuery = "SELECT * from User_Role WHERE ID ='" + userRoleID + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_IsExistingUserRoleForUpdateAndDelete", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRoleID", userRoleID);

                    reader = await cmd.ExecuteReaderAsync();
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

        public static async Task<CommonResponse> AddNewUserRole(UserRoleModel userRole)
        {
            CommonResponse response = new CommonResponse();
            var dateCreated = DateTime.Now;
            //String SQL = "INSERT INTO User_Role(Name,DateAdded)" +
                //"VALUES('" + userRole.Name + "','" + dateCreated + "')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_AddNewUserRole", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRole", userRole.Name);
                    cmd.Parameters.AddWithValue("@DateAdded", dateCreated);

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

        public static async Task<List<UserRoleModel>> GetUserRoles()
        {
            List<UserRoleModel> UserRoles = new List<UserRoleModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingUserQuery = "SELECT * from User_Role";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetUserRoles", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserRoleModel roleItem = new UserRoleModel();
                            roleItem.ID = reader.GetInt32(0);
                            roleItem.Name = reader.GetString(1);
                            roleItem.DateAdded = reader.GetDateTime(2);
                            UserRoles.Add(roleItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    UserRoles = null;
                }
                finally
                {
                    dbConn.Close();
                }

                return UserRoles;
            }
        }


        public static async Task<CommonResponse> UpdateRole(UserRoleModel userRole)
        {
            CommonResponse response = new CommonResponse();
            //String SQL = "UPDATE User_Role SET Name = '" + role.Name + "' WHERE ID = '" + role.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_UpdateUserRole", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRole", userRole.Name);
                    cmd.Parameters.AddWithValue("@RoleID", userRole.ID);

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


        public static async Task<CommonResponse> DeleteRole(UserRoleModel userRole)
        {
            CommonResponse response = new CommonResponse();
            //String SQL = "DELETE FROM User_Role WHERE Name = '" + role.Name + "' AND ID = '" + role.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_DeleteUserRole", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRole", userRole.Name);
                    cmd.Parameters.AddWithValue("@RoleID", userRole.ID);

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