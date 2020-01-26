using HospitalManagementSystem.Models.Doctor;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Doctor
{
    public class DoctorService : BaseAppTenant
    {
        
        public static async Task<List<DoctorModel>> GetDoctors()
        {
            List<DoctorModel> Doctors = new List<DoctorModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "Select * From [User] Where RoleID = 2";
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
                            DoctorModel doctorItem = new DoctorModel();
                            doctorItem.ID = reader.GetInt32(0);
                            doctorItem.Name = reader.GetString(1);
                            Doctors.Add(doctorItem);
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

                return Doctors;
            }
        }


        public static async Task<List<DoctorModel>> SearchDoctors(DoctorModel doctor)
        {
            List<DoctorModel> Doctors = new List<DoctorModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from [User] WHERE Name LIKE '" + doctor.Name + "%' AND RoleID = 2";
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
                            while (reader.Read())
                            {
                                DoctorModel doctorItem = new DoctorModel();
                                doctorItem.ID = reader.GetInt32(0);
                                doctorItem.Name = reader.GetString(1);
                                Doctors.Add(doctorItem);
                            }
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

                return Doctors;
            }
        }

        //public static async Task<bool> AddDoctor(DoctorModel doctor)
        //{
        //    String SQL = "INSERT INTO Doctor(Name,NIC,Contact,Email,Address,UserID,CatergoryID)" +
        //        "VALUES('" + doctor.Name + "','" + doctor.NIC + "','" + doctor.Contact + "','" + doctor.Email + "','" + doctor.Address + "','" + doctor.UserID + "','" + doctor.CategoryID + "')";

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

        //public static async Task<bool> UpdateDoctor(DoctorModel doctor)
        //{
        //    String SQL = "UPDATE Doctor SET Name = '" + doctor.Name + "', Contact = '" + doctor.Contact + "', Address = '" + doctor.Address + "', NIC = '" + doctor.NIC + "', WHERE ID = '" + doctor.ID + "'";

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

        //public static async Task<bool> DeleteDoctor(DoctorModel doctor)
        //{
        //    String SQL = "DELETE from Doctor WHERE ID = '" + doctor.ID + "' AND NIC = '" + doctor.NIC + "'";

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