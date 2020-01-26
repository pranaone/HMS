using HospitalManagementSystem.Models.Patient;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Patient
{
    public class PatientHistoryService : BaseAppTenant
    {
        public static async Task<List<PatientHistoryModel>> GetPatientHistory()
        {
            List<PatientHistoryModel> PatientHistories = new List<PatientHistoryModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Patient_History";
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
                            PatientHistoryModel patientHistoryItem = new PatientHistoryModel();
                            patientHistoryItem.ID = reader.GetInt32(0);
                            patientHistoryItem.PatientID = reader.GetInt32(1);
                            patientHistoryItem.NIC = reader.GetString(2);
                            patientHistoryItem.Symptoms = reader.GetString(3);
                            patientHistoryItem.Diagnosis = reader.GetString(4);
                            patientHistoryItem.Changes = reader.GetString(5);
                            patientHistoryItem.Remarks = reader.GetString(6);
                            patientHistoryItem.LabReport = reader.GetString(7);
                            patientHistoryItem.Prescription = reader.GetString(8);
                            patientHistoryItem.DateAdded = reader.GetDateTime(9);
                            patientHistoryItem.DateModified = reader.GetDateTime(10);
                            PatientHistories.Add(patientHistoryItem);
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

                return PatientHistories;
            }
        }

        public static async Task<List<PatientHistoryModel>> SearchPatientHistory(PatientHistoryModel patientHistory)
        {
            List<PatientHistoryModel> PatientHistories = new List<PatientHistoryModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT Patient_History.ID, PatientID,Patient.Name,Patient.NIC,Symptoms,Diagnosis,Changes,Remarks,LabReport,Prescription,DateAdded,DateModified FROM Patient_History INNER JOIN Patient ON Patient_History.PatientID = Patient.ID  WHERE Patient.NIC = '"+patientHistory.NIC+"'";
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
                            PatientHistoryModel patientHistoryItem = new PatientHistoryModel();
                            patientHistoryItem.ID = reader.GetInt32(0);
                            patientHistoryItem.PatientID = reader.GetInt32(1);
                            patientHistoryItem.Name = reader.GetString(2);
                            patientHistoryItem.NIC = reader.GetString(3);
                            patientHistoryItem.Symptoms = reader.GetString(4);
                            patientHistoryItem.Diagnosis = reader.GetString(5);
                            patientHistoryItem.Changes = reader.GetString(6);
                            patientHistoryItem.Remarks = reader.GetString(7);
                            patientHistoryItem.LabReport = reader.GetString(8);
                            patientHistoryItem.Prescription = reader.GetString(9);
                            patientHistoryItem.DateAdded = reader.GetDateTime(10);
                            patientHistoryItem.DateModified = reader.GetDateTime(11);
                            PatientHistories.Add(patientHistoryItem);
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

                return PatientHistories;
            }
        }

        public static async Task<bool> AddPatientHistory(PatientHistoryModel patientHistory)
        {
            string SQL = "INSERT INTO Patient_History(PatientID,NIC,Symptoms,Diagnosis,Changes,Remarks,LabReport,Prescription,DateAdded,DateModified)" +
                "VALUES(@patID,@NIC,@sym,@diag,@chgs,@rem,@labRep,@pres,@dateAdd,@dateMod)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@patID", patientHistory.PatientID);
                    cmd.Parameters.AddWithValue("@NIC", patientHistory.NIC);
                    cmd.Parameters.AddWithValue("@sym", patientHistory.Symptoms);
                    cmd.Parameters.AddWithValue("@diag", patientHistory.Diagnosis);
                    cmd.Parameters.AddWithValue("@chgs", patientHistory.Changes);
                    cmd.Parameters.AddWithValue("@rem", patientHistory.Remarks);
                    cmd.Parameters.AddWithValue("@labRep", patientHistory.LabReport);
                    cmd.Parameters.AddWithValue("@pres", patientHistory.Prescription);
                    cmd.Parameters.AddWithValue("@dateAdd", patientHistory.DateAdded);
                    cmd.Parameters.AddWithValue("@dateMod", patientHistory.DateModified);
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

        public static async Task<bool> UpdatePatientHistory(PatientHistoryModel patientHistory)
        {
            string SQL = "UPDATE Patient_History SET Changes = @chgs, Remarks = @rem, Prescription = @pres, DateModified = @dateMod WHERE PatientID = @patID";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    //cmd.Parameters.AddWithValue("@ID", patientHistory.ID);
                    cmd.Parameters.AddWithValue("@patID", patientHistory.PatientID);
                    //cmd.Parameters.AddWithValue("@NIC", patientHistory.NIC);
                    //cmd.Parameters.AddWithValue("@sym", patientHistory.Symptoms);
                    //cmd.Parameters.AddWithValue("@diag", patientHistory.Diagnosis);
                    cmd.Parameters.AddWithValue("@chgs", patientHistory.Changes);
                    cmd.Parameters.AddWithValue("@rem", patientHistory.Remarks);
                    //cmd.Parameters.AddWithValue("@labRep", patientHistory.LabReport);
                    cmd.Parameters.AddWithValue("@pres", patientHistory.Prescription);
                    //cmd.Parameters.AddWithValue("@dateAdd", patientHistory.DateAdded);
                    cmd.Parameters.AddWithValue("@dateMod", patientHistory.DateModified);
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
       
    }
}