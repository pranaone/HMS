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
    public class AddmissionService : BaseAppTenant
    {
        public static async Task<List<WardModel>> GetAvailableDoc()
        {
            List<WardModel> wards = new List<WardModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Ward";
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


        //public static async Task<List<InPatientModel>> GetInPatients_all(int id)
        //{
        //    List<InPatientModel> inpatients = new List<InPatientModel>();
        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        var query = "SELECT * from In_Patient"; 
        //        SqlDataReader reader;

        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand(query, dbConn);
        //            reader = await cmd.ExecuteReaderAsync();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    InPatientModel inpatinetItem = new InPatientModel();
        //                    inpatinetItem.ID = reader.GetInt32(0);
        //                    inpatinetItem.PatientID = reader.GetInt32(1);
        //                    inpatinetItem.AdmissionDate = reader.GetDateTime(5);
        //                    inpatinetItem.AdmissionFee = reader.GetInt32(4);
        //                    inpatinetItem.RoomID = reader.GetDateTime(5);
        //                    inpatinetItem.DischargeDate = reader.GetDateTime(5);
        //                    inpatients.Add(inpatinetItem);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            reader = null;
        //            Console.WriteLine(ex);

        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }

        //        return inpatients;
        //    }
        //}

        //public static async Task<List<InPatientModel>> GetInPatients(int id)    
        //{
        //    List<InPatientModel> inpatients = new List<InPatientModel>();
        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        var query = "SELECT * from In_Patient where Patient_ID=" + id;
        //        SqlDataReader reader;

        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand(query, dbConn);
        //            reader = await cmd.ExecuteReaderAsync();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    InPatientModel inpatinetItem = new InPatientModel();
        //                    inpatinetItem.ID = reader.GetInt32(0);
        //                    inpatinetItem.PatientID = reader.GetInt32(1);
        //                    inpatinetItem.DoctorID = reader.GetInt32(2);
        //                    inpatinetItem.RoomID = reader.GetInt32(3);
        //                    inpatinetItem.WardID = reader.GetInt32(4);
        //                    inpatinetItem.DateAddmitted = reader.GetDateTime(5);
        //                    inpatinetItem.DateDischarged = reader.GetDateTime(5);
        //                    inpatients.Add(inpatinetItem);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            reader = null;
        //            Console.WriteLine(ex);

        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }

        //        return inpatients;
        //    }
        //}

        //public static async Task<List<AddmissionModel>> GetWardDetails()
        //{
        //    List<AddmissionModel> wards = new List<AddmissionModel>();
        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        var query = "SELECT Ward.ID, Ward.Name, Doctor.ID, Doctor.Name,Room.ID, Room.Name, Room.Price FROM Room INNER JOIN Ward ON Room.WardID = Ward.ID INNER JOIN Doctor ON Ward.DoctorID = Doctor.ID WHERE Room.IsAvailable = 1";
        //        SqlDataReader reader;

        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand(query, dbConn);
        //            reader = await cmd.ExecuteReaderAsync();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    AddmissionModel wardItem = new AddmissionModel();
        //                    wardItem.WardID = reader.GetInt32(0);
        //                    wardItem.WardName = reader.GetString(1);
        //                    wardItem.DoctorID = reader.GetInt32(2);
        //                    wardItem.DoctorName = reader.GetString(3);
        //                    wardItem.RoomID = reader.GetInt32(4);
        //                    wardItem.RoomName = reader.GetString(5);
        //                    wardItem.RoomPrice = reader.GetDecimal(6);
        //                    wards.Add(wardItem);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            reader = null;
        //            Console.WriteLine(ex);

        //        }
        //        finally
        //        {
        //            dbConn.Close();
        //        }

        //        return wards;
        //    }
        //}


        public static async Task<bool> AddInPatient(InPatientModel inpatient)
        {
            String sql = "insert into In_Patient(Patient_ID,AddmittedDate, RoomID) values('"+inpatient.PatientID+"',@dateADD , '"+inpatient.RoomID+"')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@dateADD", inpatient.DateAddmitted);
                    await cmd.ExecuteNonQueryAsync();
                    dbConn.Close();
                    await RoomService.UpdateRoomAvailability(inpatient.RoomID);
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

        ////discharge patient
        //public static async Task<bool> UpdateInPatient(InPatientModel inpatient)
        //{
        //    String SQL = "UPDATE In_Patient SET Discharged = @disDate WHERE PatientID = @ID";

        //    using (SqlConnection dbConn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            dbConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = SQL;
        //            cmd.Connection = dbConn;
        //            cmd.Parameters.AddWithValue("@ID", inpatient.PatientID);
        //            cmd.Parameters.AddWithValue("@disDate", inpatient.DateDischarged);
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


        //update the discharge date and room status
        public static async Task<bool> UpdateDisDateRoomStatus(DischargeBillModel discharge)
        {
            String SQL1 = "UPDATE Room SET IsAvailable = 1 WHERE ID =" + discharge.RoomID;
            String SQL2 = "UPDATE In_Patient SET DischargedDate = @dateDis where ID ='"+ discharge.AdmissionID + "'";


            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    //update the room status
                    dbConn.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = SQL1;
                    cmd1.Connection = dbConn;
                    await cmd1.ExecuteNonQueryAsync();
                    dbConn.Close();

                    //update the discharge date
                    dbConn.Open();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = SQL2;
                    cmd2.Connection = dbConn;
                    cmd2.Parameters.AddWithValue("@dateDis", discharge.DischargedDate);
                    await cmd2.ExecuteNonQueryAsync();
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




        //public static async Task<bool> DeleteInPatient(InPatientModel inPatient)
        //{
        //    String SQL = "DELETE from In_Patient WHERE ID = '" + inPatient.ID + "'";

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