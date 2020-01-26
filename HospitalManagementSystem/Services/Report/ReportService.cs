using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Report;
using HospitalManagementSystem.Services.BaseService;

namespace HospitalManagementSystem.Services.ReportService
{
    public class ReportService : BaseAppTenant
    {
        public static async Task<bool> ReportExists(ReportModel report)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportExist = "SELECT * from Report WHERE ID = '" + report.ID + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isReportExist, dbConn);
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

        
        public static async Task<List<PatientReportModel>> GetReports(ReportModel report)
        {
            List<PatientReportModel> Reports = new List<PatientReportModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetAllPatientReports", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PatientReportModel reportItem = new PatientReportModel();
                            reportItem.ID = reader.GetInt32(0);
                            reportItem.PatientID = reader.GetString(1);
                            reportItem.ReportType = reader.GetString(2);
                            reportItem.Results = reader.GetString(3);
                            reportItem.SampleDate = reader.GetDateTime(4);
                            reportItem.TestedDate = reader.GetDateTime(5);
                            reportItem.Remarks = reader.GetString(6);
                            reportItem.Fee = reader.GetDecimal(7);
                            reportItem.ReportHtml = reader.GetString(8);
                            Reports.Add(reportItem);
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

                return Reports;
            }
        }


        public static async Task<List<PatientReportModel>> GetPatientReport(ReportModel report)
        {
            List<PatientReportModel> Reports = new List<PatientReportModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                //var query = "SELECT * from Report";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetPatientReports", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", report.PatientID);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PatientReportModel reportItem = new PatientReportModel();
                            reportItem.ID = reader.GetInt32(0);
                            reportItem.PatientID = reader.GetString(1);
                            reportItem.ReportType = reader.GetString(2);
                            reportItem.Results = reader.GetString(3);
                            reportItem.SampleDate = reader.GetDateTime(4);
                            reportItem.TestedDate = reader.GetDateTime(5);
                            reportItem.Remarks = reader.GetString(6);
                            reportItem.Fee = reader.GetDecimal(7);
                            reportItem.ReportHtml = reader.GetString(8);
                            Reports.Add(reportItem);
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

                return Reports;
            }
        }


        public static async Task<bool> AddReport(ReportModel report)
        {
            String htmlContent = Base64Encode(report.ReportHtml);
            String SQL = "INSERT INTO Report(PatientID, ReportType, Results, SampleDate, TestedDate, Remarks, Fee, ReportHtml) " +
                "VALUES(@patID,@type,@res,@sDate,@tDate,@rem,@fee,@html)";
                                       
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@patID", report.PatientID);
                    cmd.Parameters.AddWithValue("@type", report.ReportType);
                    cmd.Parameters.AddWithValue("@res", report.Results);
                    cmd.Parameters.AddWithValue("@sDate", report.SampleDate);
                    cmd.Parameters.AddWithValue("@tDate", report.TestedDate);
                    cmd.Parameters.AddWithValue("@rem", report.Remarks);
                    cmd.Parameters.AddWithValue("@fee", report.Fee);
                    cmd.Parameters.AddWithValue("@html", report.ReportHtml);
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

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static async Task<bool> ReportTypeExists(ReportTypeModel reportType)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportTypeExist = "SELECT * from ReportType WHERE Name = '" + reportType.Name + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isReportTypeExist, dbConn);
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

        public static async Task<CommonResponse> AddNewReportType(ReportTypeModel reportType)
        {
            CommonResponse response = new CommonResponse();
            var dateCreated = DateTime.Now;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();

                    SqlCommand cmd = new SqlCommand("SP_AddNewReportType", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReportType", reportType.Name);
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

        public static async Task<List<ReportTypeModel>> GetReportTypes()
        {
            List<ReportTypeModel> ReportTypes = new List<ReportTypeModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetReportTypes", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ReportTypeModel reportItem = new ReportTypeModel();
                            reportItem.ID = reader.GetInt32(0);
                            reportItem.Name = reader.GetString(1);
                            reportItem.DateAdded = reader.GetDateTime(2);
                            ReportTypes.Add(reportItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ReportTypes = null;
                }
                finally
                {
                    dbConn.Close();
                }

                return ReportTypes;
            }
        }

        public static async Task<bool> ReportTypeExistsForUpdateAndDelete(int reportTypeID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand("SP_IsExistingReportTypeForUpdateAndDelete", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReportTypeID", reportTypeID);

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

        public static async Task<CommonResponse> UpdateReportType(ReportTypeModel reportType)
        {
            CommonResponse response = new CommonResponse();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();

                    SqlCommand cmd = new SqlCommand("SP_UpdateReportType", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReportType", reportType.Name);
                    cmd.Parameters.AddWithValue("@ReportTypeID", reportType.ID);

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


        public static async Task<CommonResponse> DeleteReportType(ReportTypeModel reportType)
        {
            CommonResponse response = new CommonResponse();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();

                    SqlCommand cmd = new SqlCommand("SP_DeleteReportType", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReportType", reportType.Name);
                    cmd.Parameters.AddWithValue("@ReportTypeID", reportType.ID);

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



        //public static async Task<bool> UpdateReport(ReportModel report)
        //{
        //    String SQL = "UPDATE Report SET ReportHtml = '" + report.Report + "' WHERE ID = '" + report.ID + "'";

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

        //public static async Task<bool> DeleteReport(ReportModel report)
        //{
        //    String SQL = "DELETE from Party WHERE ID = '" + report.ID + "' AND PatientID = '" + report.PatientID + "'";

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