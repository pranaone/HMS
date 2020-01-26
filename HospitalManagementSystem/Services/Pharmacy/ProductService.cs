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
    public class ProductService: BaseAppTenant
    {
        public static async Task<bool> AddProduct(ProductModel product)
        {
            var dateCreated = DateTime.Now;
            String SQL = "INSERT INTO Products(Name, Description, UnitPrice, DateAdded)" +
                "VALUES('" + product.Name + "','" + product.Description + "','" + product.UnitPrice + "', @dateAdd)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@dateAdd", product.DateAdded);
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

        public static async Task<bool> ProductExist(int productID)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isProductExists = "SELECT * from Products WHERE ID = '" + productID + "'";
                SqlDataReader reader;

                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand(isProductExists, dbConn);
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


        public static async Task<bool> UpdateProduct(ProductModel product)
        {
            String SQL = "UPDATE Products SET Name = '" + product.Name + "', Description = '" + product.Description + "', UnitPrice = '" + product.UnitPrice + "'  WHERE ID = '" + product.ID + "'";

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

        public static async Task<bool> DeleteProduct(ProductModel product)
        {
            String SQL = "DELETE from Products WHERE ID = '" + product.ID + "'";

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
        }//end of delete method


        public static async Task<List<ProductModel>> GetProducts(ProductModel product)
        {
            List<ProductModel> Product = new List<ProductModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT ID, Name, Description, UnitPrice, DateAdded from Products ORDER BY ID DESC";
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
                            ProductModel reportItem = new ProductModel();
                            reportItem.ID = reader.GetInt32(0);
                            reportItem.Name = reader.GetString(1);
                            reportItem.Description = reader.GetString(2);
                            reportItem.UnitPrice = reader.GetDecimal(3);
                            reportItem.DateAdded = reader.GetDateTime(4);
                            Product.Add(reportItem);
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

                return Product;
            }
        }//end of display 

    }
}