using HospitalManagementSystem.Models.Appointment;
using HospitalManagementSystem.Models.Fee;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Appointment
{
    public class AppointmentService : BaseAppTenant
    {
        public static async Task<bool> AppointmentExists(AppointmentModel appointment)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Appointment WHERE ID = '" + appointment.ID + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query, dbConn);
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
                    Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    dbConn.Close();
                }

            }
        }


        public static async Task<List<AppointmentModel>> GetAppointments()
        {
            List<AppointmentModel> Appointments = new List<AppointmentModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Appointment";
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
                            AppointmentModel AppointmentItem = new AppointmentModel();
                            AppointmentItem.ID = reader.GetInt32(0);
                            AppointmentItem.PatientID = reader.GetInt32(1);
                            AppointmentItem.DoctorID = reader.GetInt32(2);
                            AppointmentItem.Date = reader.GetDateTime(3);
                            AppointmentItem.Fee = reader.GetDecimal(4);
                            Appointments.Add(AppointmentItem);
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

                return Appointments;
            }
        }

        public static async Task<List<FeeModel>> GetFees()
        {
            List<FeeModel> Fee = new List<FeeModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Fee";
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
                            FeeModel FeeItem = new FeeModel();
                            FeeItem.ID = reader.GetInt32(0);
                            FeeItem.Description = reader.GetString(1);
                            FeeItem.Fee = reader.GetDecimal(2);
                            Fee.Add(FeeItem);
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

                return Fee;
            }
        }

        public static async Task<bool> AddAppointment(AppointmentModel appointment)
        {
            String SQL = "INSERT INTO Appointment(PatientID,DoctorID,Date,Fee) VALUES(@patID,@docID,@appDate,@appFee)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@patID", appointment.PatientID);
                    cmd.Parameters.AddWithValue("@docID", appointment.DoctorID);
                    cmd.Parameters.AddWithValue("@appDate", appointment.Date);
                    cmd.Parameters.AddWithValue("@appFee", appointment.Fee);
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
        }

        public static async Task<bool> UpdateAppointment(AppointmentModel appointment)
        {
            String SQL = "UPDATE Appointment SET PatientID = '" + appointment.PatientID + "', DoctorID = '" + appointment.DoctorID + "', Date = '" + appointment.Date + "', Amount = '" + appointment.Fee + "' WHERE ID = '" + appointment.ID + "'";

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
        }

        public static async Task<bool> DeleteAppointment(AppointmentModel appointment)
        {
            String SQL = "DELETE from Appointment WHERE ID = '" + appointment.ID + "'";

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
        }

        public static async Task<int> GetAppointmentCount(AppointmentModel appointment)
        {
            
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {

                String SQL = "SELECT COUNT(*) FROM Appointment WHERE DoctorID = @docID and Date = @appDate";

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, dbConn);
                    cmd.Parameters.AddWithValue("@docID", appointment.DoctorID);
                    cmd.Parameters.AddWithValue("@appDate",appointment.Date);
                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    return count;
                    
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex);
                    return 0;
                }
                finally
                {
                    dbConn.Close();
                }
               
            }
        }

        
    }
}