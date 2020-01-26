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

namespace HospitalManagementSystem.Services.DoctorCategory
{
    public class DoctorCategoryService : BaseAppTenant
    {
        public static async Task<bool> DoctorCategoryExists(string doctorCategory)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingDoctorCategoryQuery = "SELECT * from Doctor_Category WHERE Name ='" + doctorCategory + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_IsExistingDoctorCategory", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorCategory", doctorCategory);

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


        public static async Task<bool> DoctorCategoryExistsForUpdateAndDelete(int doctorCategoryID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isExistingDoctorCategoryQuery = "SELECT * from Doctor_Category WHERE ID ='" + doctorCategoryID + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_IsExistingDoctorCategoryForUpdateAndDelete", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorCategoryID", doctorCategoryID);

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

        public static async Task<CommonResponse> AddNewDoctorCategory(DoctorCategoryModel doctorCategory)
        {
            CommonResponse response = new CommonResponse();
            var dateCreated = DateTime.Now;
            //String SQL = "INSERT INTO Doctor_Category(Name,DateAdded)" +
            //    "VALUES('" + doctorCategory.Name + "','" + dateCreated + "')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_AddNewDoctorCategory", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorCategory", doctorCategory.Name);
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


        public static async Task<List<DoctorCategoryModel>> GetDoctorCategories()
        {
            List<DoctorCategoryModel> DoctorCategories = new List<DoctorCategoryModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var isExistingUserQuery = "SELECT * from Doctor_Category";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetDoctorCategories", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DoctorCategoryModel DoctorCategory = new DoctorCategoryModel();
                            DoctorCategory.ID = reader.GetInt32(0);
                            DoctorCategory.Name = reader.GetString(1);
                            DoctorCategory.DateAdded = reader.GetDateTime(2);
                            DoctorCategories.Add(DoctorCategory);
                        }
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                }
                finally
                {
                    dbConn.Close();
                }

                return DoctorCategories;
            }
        }

        public static async Task<CommonResponse> UpdateDoctorCategory(DoctorCategoryModel docCategory)
        {
            CommonResponse response = new CommonResponse();
            //String SQL = "UPDATE Doctor_Category SET Name = '" + docCategory.Name + "' WHERE ID = '" + docCategory.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_UpdateDoctorCategory", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorCategory", docCategory.Name);
                    cmd.Parameters.AddWithValue("@DoctorCategoryID", docCategory.ID);

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


        public static async Task<CommonResponse> DeleteDoctorCategory(DoctorCategoryModel docCategory)
        {
            CommonResponse response = new CommonResponse();
            //String SQL = "DELETE FROM Doctor_Category WHERE Name = '" + docCategory.Name + "' AND ID = '" + docCategory.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = SQL;
                    //cmd.Connection = dbConn;

                    SqlCommand cmd = new SqlCommand("SP_DeleteDoctorCategory", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorCategory", docCategory.Name);
                    cmd.Parameters.AddWithValue("@DoctorCategoryID", docCategory.ID);

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