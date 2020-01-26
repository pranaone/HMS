using HospitalManagementSystem.Models.Bill;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Bill
{
    public class BillService : BaseAppTenant
    {
        public static async Task<List<BillModel>> GetBills()
        {
            List<BillModel> bills = new List<BillModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Bill";
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
                            BillModel billItem = new BillModel();
                            billItem.ID = reader.GetInt32(0);
                            billItem.PatientID = reader.GetInt32(1);
                            billItem.Description = reader.GetString(2);
                            billItem.Amount = reader.GetDecimal(3);
                            billItem.Date = reader.GetDateTime(5);
                            bills.Add(billItem);
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

                return bills;
            }
        }

        public static async Task<decimal> GetTotalBill(BillModel bill)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                String SQL = "SELECT SUM(Amount) FROM Bill WHERE PatientID = @pid and Date = @date";

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, dbConn);
                    cmd.Parameters.AddWithValue("@patID", bill.PatientID);
                    cmd.Parameters.AddWithValue("@date", bill.Date);
                    decimal totalbill = Convert.ToDecimal(await cmd.ExecuteScalarAsync());

                    return totalbill;

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

        public static async Task<bool> AddBill(BillModel bill)
        {
            String SQL = "INSERT INTO Bill(PatientID,Description,Amount,Date) VALUES(@patID,@des,@amt,@date)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@patID", bill.PatientID);
                    cmd.Parameters.AddWithValue("@des", bill.Description);
                    cmd.Parameters.AddWithValue("@amt", bill.Amount);
                    cmd.Parameters.AddWithValue("@date", bill.Date);
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

        public static async Task<bool> UpdateBill(BillModel bill)
        {
            String SQL = "UPDATE Bill SET Description = @des, Amount = @amt, Date = @date WHERE ID = @ID";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@ID", bill.ID);
                    cmd.Parameters.AddWithValue("@patID", bill.PatientID);
                    cmd.Parameters.AddWithValue("@des", bill.Description);
                    cmd.Parameters.AddWithValue("@amt", bill.Amount);
                    cmd.Parameters.AddWithValue("@date", bill.Date);
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

        public static async Task<bool> DeleteBill(BillModel bill)
        {
            String SQL = "DELETE from In_Patient WHERE ID = '" + bill.ID + "'";

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