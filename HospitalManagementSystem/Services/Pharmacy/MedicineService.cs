using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Services.BaseService;


namespace HospitalManagementSystem.Services.Pharmacy
{
    public class MedicineService: BaseAppTenant
    {
        public static async Task<bool> AddMedicine(MedicineModel medicine)
        {
            String SQL = "INSERT INTO MedItems(PID, MItem, CostPrice, SellingPrice, Quantity)" +
                "VALUES('" + medicine.Pid + "', '" + medicine.Name + "', '" + medicine.CostPrice + "', '" + medicine.SellingPrice + "', '" + medicine.Quantity + "')";

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

        public static async Task<bool> MedicineExist(MedicineModel medicine)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportExist = "SELECT * from MedItems WHERE MItem = '" + medicine.Name + "'";
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
        }//end of search 


        public static async Task<bool> UpdateMedicine(MedicineModel medicine)
        {
            String SQL = "UPDATE MedItems SET PID = '" + medicine.Pid + "', MItem='" + medicine.Name + "', SellingPrice='" + medicine.SellingPrice+ "',CostPrice='" + medicine.CostPrice + "' Quantity='" + medicine.Quantity + "' WHERE MID = '" + medicine.Mid + "'";

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

        public static async Task<bool> DeleteMedicine(MedicineModel medicine)
        {
            String SQL = "DELETE from medItems WHERE MID = '" + medicine.Mid + "'";

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


        public static async Task<List<MedicineModel>> GetMedicine(MedicineModel medicine)
        {
            List<MedicineModel> Medicine = new List<MedicineModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from MedItems";
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
                            MedicineModel reportItem = new MedicineModel();
                            reportItem.Mid = reader.GetInt64(0);
                            reportItem.Name= reader.GetString(1);
                            reportItem.Pid = reader.GetInt64(2);
                            reportItem.SellingPrice = reader.GetInt32(3);
                            reportItem.CostPrice = reader.GetInt32(4);
                            reportItem.Quantity= reader.GetInt32(5);
                            Medicine.Add(reportItem);
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

                return Medicine;
            }
        }//end of display 

    }
}