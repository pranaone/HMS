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
    public class TreatmentService: BaseAppTenant
    {

        public static async Task<AdmissionTreatmentModel> SearchAdmission(AdmissionTreatmentModel admission)
        {
            AdmissionTreatmentModel patientAdmissionitem = new AdmissionTreatmentModel();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var query = "SELECT Patient.[Name], Room.[Name], ISNULL(Treatment.[Treatment], '') AS Treatment , ISNULL(Treatment.Medicines, '') AS Medicines , " +
                //    " ISNULL(Treatment.Reports, '') AS Reports from Patient join In_Patient on In_Patient.Patient_ID = Patient.ID " +
                //    " LEFT join Room on Room.ID = In_Patient.RoomID LEFT join Treatment on Treatment.AdmissionID = In_Patient.ID" +
                //            " WHERE In_Patient.ID= '" + admission.ID + "'";

                var query = "select Patient.[Name], Room.[Name], In_Patient.AddmittedDate, Patient.ID, Room.Price, In_Patient.RoomID from In_Patient" +
                    " LEFT join Patient on In_Patient.Patient_ID = Patient.ID " +
                    " LEFT join Room on Room.ID = In_Patient.RoomID " +
                    " WHERE In_Patient.ID= '" + admission.AdmissionID + "'";
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
                            //admissionitem.TreatmentID = reader.GetInt32(0);
                            patientAdmissionitem.PatientName = reader.GetString(0);
                            patientAdmissionitem.RoomName = reader.GetString(1);
                            patientAdmissionitem.admissiondate = reader.GetDateTime(2);
                            patientAdmissionitem.PatientID = reader.GetInt32(3);
                            patientAdmissionitem.RoomPrice = reader.GetDecimal(4);
                            patientAdmissionitem.RoomID= reader.GetInt32(5);
                            //Admission.Add(admissionitem);
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

                return patientAdmissionitem;
            }
        }//end of search


        public static async Task<bool> AddTreatment(TreatmentModel treatment)
        {
            String SQL = "INSERT INTO Treatment(AdmissionID, Treatment, Medicines, Reports)" +
                "VALUES('" + treatment.AdmissionID + "','" + treatment.PtntTreatment + "','" + treatment.Medicines + "','" + treatment.Reports + "')";

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

        //search treatment details of an admission
        public static async Task<PatientTreatmentModel> GetAdmissionTreatment(PatientTreatmentModel Treatment)
        {
            PatientTreatmentModel ptnttreatment = new PatientTreatmentModel();
            ptnttreatment.PtntTreatments = new List<TreatmentModel>();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query1 = "select Patient.[Name], Room.[Name] from In_Patient " +
                    " LEFT join Patient on In_Patient.Patient_ID = Patient.ID " +
                    " LEFT join Room on Room.ID = In_Patient.RoomID " +
                    " WHERE In_Patient.ID = "+ Treatment.AdmissionID;

                var query2 = "Select * from Treatment where AdmissionID=" + Treatment.AdmissionID;

                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query1, dbConn);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //// TreatmentModel Treatmentitems = new TreatmentModel();
                            ptnttreatment.PatientName = reader.GetString(0);
                            ptnttreatment.RoomName = reader.GetString(1);
                        }
                    }
                    dbConn.Close();

                    dbConn.Open();
                    SqlCommand cmd1 = new SqlCommand(query2, dbConn);
                    reader = await cmd1.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           TreatmentModel Treatmentitems = new TreatmentModel();
                            Treatmentitems.AdmissionID = reader.GetInt32(1);
                            Treatmentitems.PtntTreatment = reader.GetString(2);
                            Treatmentitems.Medicines = reader.GetString(3);
                            Treatmentitems.Reports = reader.GetString(4);
                            ptnttreatment.PtntTreatments.Add(Treatmentitems);
                        }
                    }
                    dbConn.Close();
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

                return ptnttreatment;
            }
        }//end of search

        public static async Task<bool> UpdateTreatment(TreatmentModel treatment)
        {
            String SQL = "UPDATE Treatment SET Treatment = '" + treatment.PtntTreatment + "', " +
                "Medicines = '" + treatment.Medicines + "',Reports = '" + treatment.Reports + "'   " +
                "WHERE ID = '" + treatment.ID + "'";

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

    }
}