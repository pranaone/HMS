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
    public class InventoryService: BaseAppTenant
    {
        public static async Task<bool> AddInventory(InventoryModel inventory)
        {
            var dateUpdated = DateTime.Now;
            String SQL = "INSERT INTO Inventory(ProductID, Quantity, Description, DateUpdated)" +
                "VALUES('" + inventory.ProductID + "', '" + inventory.Quantity + "', '" + inventory.Description + "',@dateAdd)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@dateAdd", dateUpdated);
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

        public static async Task<bool> InventoryExist(InventoryModel inventory)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportExist = "SELECT * from Inventory WHERE IID = '" + inventory.ID + "'";
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


        public static async Task<bool> UpdateInventory(InventoryModel inventory)
        {
            String SQL = "UPDATE Inventory SET ProductID = '" + inventory.ProductID + "', Quantity='" + inventory.Quantity + "' WHERE ID = '" + inventory.ID + "'";

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

        public static async Task<bool> DeleteInventory(InventoryModel inventory)
        {
            String SQL = "DELETE from Inventory WHERE ID = '" + inventory.ID + "'";

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


        public static async Task<List<InventoryQuantityModel>> GetInventory(InventoryModel inventory)
        {
            List<InventoryQuantityModel> Inventory = new List<InventoryQuantityModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = @"SELECT p.[Name], SUM(i.Quantity) AS Quantity
                                   FROM Inventory i
                                   JOIN Products p on p.ID = i.ProductID
                                    GROUP BY p.[Name]";
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
                            InventoryQuantityModel inventoryItem = new InventoryQuantityModel();
                            inventoryItem.Name = reader.GetString(0);
                            inventoryItem.Quantity = reader.GetDecimal(1);
                            Inventory.Add(inventoryItem);
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

                return Inventory;
            }
        }//end of display method


        public static async Task<List<InventoryQuantityModel>> GetSingleInventory(InventoryModel inventory)
        {
            List<InventoryQuantityModel> Inventory = new List<InventoryQuantityModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT p.[Name], SUM(i.Quantity) AS Quantity, p.UnitPrice" +
                                   " FROM Inventory i " +
                                   " JOIN Products p on p.ID = i.ProductID " +
                                   " WHERE i.ProductID = '" + inventory.ProductID  + "'" +
                                    " GROUP BY p.[Name], p.UnitPrice";
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
                            InventoryQuantityModel inventoryItem = new InventoryQuantityModel();
                            inventoryItem.Name = reader.GetString(0);
                            inventoryItem.Quantity = reader.GetDecimal(1);
                            inventoryItem.UnitPrice = reader.GetDecimal(2);
                            Inventory.Add(inventoryItem);
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

                return Inventory;
            }
        }//end of display method


        public static async Task<bool> UpdateInventoryAfterSales(int cartHeaderID)
        {
            List<CartDetailsModel> cartDetailsToDelete = new List<CartDetailsModel>();
            var query = "SELECT * FROM CartDetails WHERE CartHeaderID ='" + cartHeaderID + "'";

            SqlDataReader reader;


            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartDetailsModel cartDetailItem = new CartDetailsModel();
                            cartDetailItem.CartDetailID = reader.GetInt32(0);
                            cartDetailItem.UserID = reader.GetInt32(1);
                            cartDetailItem.CartHeaderID = reader.GetInt32(2);
                            cartDetailItem.ProductID = reader.GetInt32(3);
                            cartDetailItem.Quantity = reader.GetDecimal(4);
                            cartDetailItem.UnitPrice = reader.GetDecimal(5);
                            cartDetailItem.TotalPrice = reader.GetDecimal(6);
                            cartDetailsToDelete.Add(cartDetailItem);
                        }
                    }
                    dbConn.Close();


                    foreach (var item in cartDetailsToDelete)
                    {
                        InventoryModel inventoryToAdd = new InventoryModel()
                        {
                            ProductID = item.ProductID,
                            Quantity = item.Quantity * (-1),
                            Description = "Sales"
                        };

                        await InventoryService.AddInventory(inventoryToAdd);
                    }
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


    }
}