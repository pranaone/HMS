using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Models.ViewReports;
using HospitalManagementSystem.Services.BaseService;

namespace HospitalManagementSystem.Services.Pharmacy
{
    public class SalesService: BaseAppTenant
    {
    public static async Task<bool> AddSales(SalesModel sales)
        {
            var salesDate = DateTime.Now;
            String SQL = "INSERT INTO Sales(CarHeaderID, UserID, PatientID, TotalPrice, TaxAmount, TotalBill, SalesDate)" +
                "VALUES('" + sales.CartHeaderID + "', '" + sales.UserID + "', '" + sales.PatientID + "', '" + sales.TotalPrice + "', '" + sales.TaxAmount + "', '" + sales.TotalBill + "', '" + salesDate + "')";

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


                    await InventoryService.UpdateInventoryAfterSales(sales.CartHeaderID);
                    
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

       
        public static async Task<bool> SalesExist(SalesModel sales)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isSalesExist = "SELECT * from Sales WHERE CarHeaderID = '" + sales.CartHeaderID + "' AND Date='" + sales.SalesDate + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isSalesExist, dbConn);
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
        }//end of search 


        public static async Task<bool> UpdateSales(SalesModel sales)
        {
            String SQL = "UPDATE Sales SET PatientID = '" + sales.PatientID + "', TotalPrice='" + sales.TotalPrice + "', TaxAmount='" + sales.TaxAmount + "',TotalBill='" + sales.TotalBill + "' WHERE ID = '" + sales.ID + "'";

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

        public static async Task<bool> DeleteSales(SalesModel sales)
        {
            String SQL = "DELETE from Sales WHERE ID = '" + sales.ID + "'";

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
        }//end of delete 


        public static async Task<List<SalesModel>> GetSales(SalesModel sales)
        {
            List<SalesModel> Sales = new List<SalesModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Sales";
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
                            SalesModel reportItem = new SalesModel();
                            reportItem.ID = reader.GetInt32(0);
                            reportItem.CartHeaderID = reader.GetInt32(1);
                            reportItem.PatientID = reader.GetInt32(2);
                            reportItem.UserID = reader.GetInt32(3);
                            reportItem.PatientID = reader.GetInt32(4);
                            reportItem.TotalPrice = reader.GetInt32(5);
                            reportItem.TaxAmount = reader.GetDecimal(6);
                            reportItem.TotalBill = reader.GetDecimal(7);
                            reportItem.SalesDate = reader.GetDateTime(8);
                            Sales.Add(reportItem);
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

                return Sales;
            }
        }//end of display 


        public static async Task<List<SalesReportModel>> GetSalesReport(SalesReportModel sales)
        {
            if(sales.SearchFromDate == null)
            {
                sales.SearchFromDate = "";
            }
            if (sales.SearchToDate == null)
            {
                sales.SearchToDate = "";
            }

            List<SalesReportModel> Sales = new List<SalesReportModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
               
                SqlDataReader reader;
                    
                try
                {

                    dbConn.Open();

                    SqlCommand cmd = new SqlCommand("SP_GetSalesReports", dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //passing values from the model to the SP
                    cmd.Parameters.AddWithValue("@PatientID", sales.PatientID);
                    cmd.Parameters.AddWithValue("@ProductID", sales.ProductID);
                    cmd.Parameters.AddWithValue("@SearchFromDate", sales.SearchFromDate);
                    cmd.Parameters.AddWithValue("@SearchToDate", sales.SearchToDate);
                    reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SalesReportModel reportItem = new SalesReportModel();
                            reportItem.ID = reader.GetInt32(0);
                            //reportItem.CartHeaderID = reader.GetInt32(1);
                            //reportItem.UserID = reader.GetInt32(2);
                            reportItem.PatientName = reader.GetString(1);                         
                            reportItem.TotalPrice = reader.GetDecimal(2);
                            reportItem.TotalBill = reader.GetDecimal(3);
                            reportItem.SalesDate = reader.GetDateTime(4);
                            Sales.Add(reportItem);
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

                return Sales;
            }
        }//end of display 


        public static async Task<int> GetNewCartHeader(CartHeaderModel cartHeader)
        {
            var newCartDetailItem = 0;
            var salesDate = DateTime.Now;
            String SQL = "INSERT INTO CartHeader( UserID, CreatedDate)" +
                "output INSERTED.CartHeaderID VALUES('" + cartHeader.UserID + "', '" + salesDate + "')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    newCartDetailItem = (int)cmd.ExecuteScalar();
                    dbConn.Close();

                    return newCartDetailItem;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return newCartDetailItem;
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }//end of display 
    }
}