using HospitalManagementSystem.Models.Common;
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
    public class PatientService : BaseAppTenant
    {
        public static async Task<bool> PatientExists(PatientModel patient)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Patient WHERE NIC = '" + patient.NIC + "'";
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


        public static async Task<List<PatientModel>> GetPatients()
        {
            List<PatientModel> Patients = new List<PatientModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Patient";
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
                            PatientModel patientItem = new PatientModel();
                            patientItem.ID = reader.GetInt32(0);
                            patientItem.Name = reader.GetString(1);
                            patientItem.Contact = reader.GetString(2);
                            patientItem.Address = reader.GetString(3);
                            patientItem.NIC = reader.GetString(4);
                            patientItem.isNonNIC = reader.GetBoolean(5);
                            patientItem.GuardianNIC = reader.GetString(6);
                            Patients.Add(patientItem);
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

                return Patients;
            }
        }

        public static async Task<DashboardDataModel> GetPatientsForDashboard()
        {
            DashboardDataModel dashboardDataModel = new DashboardDataModel();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var queryNoOfPatients = "SELECT Count(Id) from Patient";
                var queryNoOfPatientsAdmitted = "SELECT Count(Id) from In_Patient";
                var queryNoOfPatientsDischarged = "SELECT Count(Id) from In_Patient WHERE DischargeDate IS NOT NULL";
                var queryNoOfPatientsInHouse = "SELECT Count(Id) from In_Patient WHERE DischargeDate IS NULL";
                SqlDataReader reader1;
                SqlDataReader reader2;
                SqlDataReader reader3;
                SqlDataReader reader4;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd1 = new SqlCommand(queryNoOfPatients, dbConn);
                    reader1 = await cmd1.ExecuteReaderAsync();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            dashboardDataModel.NoOfPatients = reader1.GetInt32(0);
                        }
                    }
                    dbConn.Close();

                    dbConn.Open();
                    SqlCommand cmd2 = new SqlCommand(queryNoOfPatientsAdmitted, dbConn);
                    reader2 = await cmd2.ExecuteReaderAsync();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            dashboardDataModel.NoOfPatientsAdmitted = reader2.GetInt32(0);
                        }
                    }
                    dbConn.Close();

                    dbConn.Open();
                    SqlCommand cmd3 = new SqlCommand(queryNoOfPatientsDischarged, dbConn);
                    reader3 = await cmd3.ExecuteReaderAsync();
                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                        {
                            dashboardDataModel.NoOfPatientsDischarged = reader3.GetInt32(0);
                        }
                    }
                    dbConn.Close();

                    dbConn.Open();
                    SqlCommand cmd4 = new SqlCommand(queryNoOfPatientsInHouse, dbConn);
                    reader4 = await cmd4.ExecuteReaderAsync();
                    if (reader4.HasRows)
                    {
                        while (reader4.Read())
                        {
                            dashboardDataModel.NoOfPatientsInHouse = reader4.GetInt32(0);
                        }
                    }
                    dbConn.Close();
                }
                catch (Exception ex)
                {
                    reader1 = null;
                    reader2 = null;
                    reader3 = null;
                    reader4 = null;
                    Console.WriteLine(ex);

                }
                finally
                {
                    dbConn.Close();
                }

                return dashboardDataModel;
            }


        }


        public static async Task<List<PatientModel>> SearchPatient(PatientModel patient)
        {
            List<PatientModel> Patients = new List<PatientModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Patient WHERE Name LIKE '%"+patient.Name+"%'";
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
                            PatientModel patientItem = new PatientModel();
                            patientItem.ID = reader.GetInt32(0);
                            patientItem.Name = reader.GetString(1);
                            patientItem.Contact = reader.GetString(2);
                            patientItem.Address = reader.GetString(3);
                            patientItem.NIC = reader.GetString(4);
                            patientItem.isNonNIC = reader.GetBoolean(5);
                            patientItem.GuardianNIC = reader.GetString(6);
                            Patients.Add(patientItem);
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

                return Patients;
            }
        }

        public static async Task<List<PatientAdmissionModel>> SearchPatientForAdmission(PatientAdmissionModel patient)
        {
            List<PatientAdmissionModel> Patients = new List<PatientAdmissionModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "select p.ID, p.[Name], ip.Addmitted, r.Price  from Patient p " +
                                " join In_Patient ip on p.ID = ip.PatientID join Room r on r.ID = ip.RoomID " +
                                " WHERE ip.Discharged = '1900-01-01 00:00:00.000' AND p.[Name] LIKE '%" + patient.Name + "%'";
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
                            PatientAdmissionModel patientItem = new PatientAdmissionModel();
                            patientItem.ID = reader.GetInt32(0);
                            patientItem.Name = reader.GetString(1);
                            patientItem.AdmittedDate = reader.GetDateTime(2);
                            patientItem.RoomPrice = reader.GetDecimal(3);
                            Patients.Add(patientItem);
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

                return Patients;
            }
        }


        public static async Task<List<PatientModel>> SearchPatientNIC(PatientModel patient)
        {
            List<PatientModel> Patients = new List<PatientModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Patient WHERE NIC = '" + patient.NIC + "'";
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
                            PatientModel patientItem = new PatientModel();
                            patientItem.ID = reader.GetInt32(0);
                            patientItem.Name = reader.GetString(1);
                            patientItem.Contact = reader.GetString(2);
                            patientItem.Address = reader.GetString(3);
                            patientItem.NIC = reader.GetString(4);
                            patientItem.isNonNIC = reader.GetBoolean(5);
                            patientItem.GuardianNIC = reader.GetString(6);
                            Patients.Add(patientItem);
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

                return Patients;
            }
        }

        public static async Task<bool> AddPatient(PatientModel patient)
        {
            String SQL = "INSERT INTO Patient(Name,Contact,Address,NIC,IsNonNIC,GuardianNIC)" +
                "VALUES('" + patient.Name + "','" + patient.Contact + "','" + patient.Address + "','" + patient.NIC + "','" + patient.isNonNIC + "','" + patient.GuardianNIC + "')";

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

        public static async Task<bool> UpdatePatient(PatientModel patient)
        {
            String SQL = "UPDATE Patient SET Name = '" + patient.Name + "', Contact = '" + patient.Contact + "', Address = '" + patient.Address + "', NIC = '" + patient.NIC + "', IsNonNIC = '" + patient.isNonNIC + "',  GuardianNIC = '" + patient.GuardianNIC + "' WHERE ID = '" + patient.ID + "'"; //

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

        public static async Task<bool> DeletePatient(PatientModel patient)
        {
            String SQL = "DELETE from Patient WHERE ID = '" + patient.ID + "' AND NIC = '" + patient.NIC + "'";

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
    }
}