using HospitalManagementSystem.Models.Addmission;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Addmission
{
    public class WardService : BaseAppTenant
    {
        public static async Task<List<WardModel>> GetWards()
        {
            List<WardModel> wards = new List<WardModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "Select * from Ward";
            //   var query = "SELECT u.Firstname FROM [User] INNER JOIN " +
               //     "Ward ON u.ID = Ward.DoctorID WHERE Room.IsAvailable = 1";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WardModel wardItems = new WardModel();
                            wardItems.ID = reader.GetInt32(0);
                            wardItems.Name = reader.GetString(1);
                            wardItems.DoctorID = reader.GetInt32(2);
                            wards.Add(wardItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    Console.WriteLine(ex);

                }
                finally
                {
                    dbConn.Close();
                }

                return wards;
            }
        }

        public static async Task<List<WardModel>> GetDocByWardID(WardModel ward)
        {
            List<WardModel> wards = new List<WardModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "select u.ID, CONCAT(u.Firstname, ' ' , u.Lastname) AS [Name] from [User] u " +
                    "join Ward w on w.DoctorID = u.ID " +
                    "where w.ID ="+ ward.ID;
                
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WardModel wardItems = new WardModel();
                            //roomItems.ID = reader.GetInt32(0);
                            //roomItems.Name = reader.GetString(1);
                            wardItems.DocName = reader.GetString(1);
                            //wardItems.DoctorID = reader.GetInt32(2);
                            //roomItems.IsAvailable = reader.GetBoolean(3);
                            //roomItems.WardID = reader.GetInt32(2);
                            wards.Add(wardItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    Console.WriteLine(ex);

                }
                finally
                {
                    dbConn.Close();
                }

                return wards;
            }
        }

        public static async Task<bool> AddWard(WardModel ward)
        {
            String SQL = "INSERT INTO Ward(Name, DoctorID)" +
                "VALUES('" + ward.Name + "','" + ward.DoctorID + "')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    await cmd.ExecuteNonQueryAsync();
                    dbConn.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }//end of add

        //public static async Task<bool> AddWard(WardModel ward)
        //{
        //    String SQL = "INSERT INTO Ward(Name,DoctorID) VALUES(@Name,@DocID)";

        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = SQL;
        //            cmd.Connection = dbConn;
        //            cmd.Parameters.AddWithValue("@Name", ward.Name);
        //            cmd.Parameters.AddWithValue("@DocID", ward.DoctorID);
        //            await cmd.ExecuteNonQueryAsync();
        //            dbConn.Close();

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return false;
        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }
        //    }
        //}

        //fetch all the available doctors who havent been assigned to a ward
        public static async Task<List<WardModel>> GetAvailableDoc()
        {
            List<WardModel> wards = new List<WardModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {

                // var query = "SELECT * FROM [User] u LEFT JOIN Ward w on w.DoctorID = u.ID WHERE u.RoleID = 2 AND w.DoctorID IS NULL";
                var query = "SELECT u.ID, CONCAT(u.Firstname, ' ', u.Lastname) As DoctorName FROM [User] u LEFT JOIN Ward w on w.DoctorID = u.ID WHERE u.RoleID = 2 AND w.DoctorID IS NULL";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WardModel wardItems = new WardModel();
                            //the index is entered according to the query
                            wardItems.DocName = reader.GetString(1);
                            wardItems.DoctorID = reader.GetInt32(0);

                            wards.Add(wardItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    reader = null;
                    Console.WriteLine(ex);

                }
                finally
                {
                    dbConn.Close();
                }

                return wards;
            }
        }

        //public static async Task<bool> UpdateWard(WardModel ward)
        //{
        //    String SQL = "UPDATE Ward SET Name = @Name, DoctorID = @DocID WHERE ID = @ID";

        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = SQL;
        //            cmd.Connection = dbConn;
        //            cmd.Parameters.AddWithValue("@Name", ward.Name);
        //            cmd.Parameters.AddWithValue("@disDate", ward.DoctorID);
        //            cmd.Parameters.AddWithValue("@disDate", ward.ID);
        //            await cmd.ExecuteNonQueryAsync();
        //            dbConn.Close();

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return false;
        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }
        //    }
        //}


        public static async Task<bool> UpdateWard(WardModel ward)
        {
            String SQL = "UPDATE Ward SET Name = '" + ward.Name + "'  WHERE ID = '" + ward.ID + "'";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    await cmd.ExecuteNonQueryAsync();
                    dbConn.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }//end of update 

        //public static async Task<bool> UpdateWard(WardModel ward)
        //{
        //    String SQL = "UPDATE Ward SET Name = @Name WHERE ID = @ID";

        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = SQL;
        //            cmd.Connection = dbConn;
        //            cmd.Parameters.AddWithValue("@Name", ward.Name);
        //            await cmd.ExecuteNonQueryAsync();
        //            dbConn.Close();

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return false;
        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }
        //    }
        //}


        //public static async Task<bool> DeleteWard(WardModel ward)
        //{
        //    String SQL = "DELETE from Ward WHERE ID = '" + ward.ID + "'";

        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = SQL;
        //            cmd.Connection = dbConn;
        //            await cmd.ExecuteNonQueryAsync();
        //            dbConn.Close();

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return false;
        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }
        //    }
        //}

    }
}