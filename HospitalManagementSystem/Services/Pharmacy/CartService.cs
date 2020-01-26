using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementSystem.Services.Pharmacy
{
    public class CartService : BaseAppTenant
    {
        public static async Task<bool> AddToCart(CartDetailsModel cartDetails)
        {
            var dateAdded = DateTime.Now;
            String SQL = "INSERT INTO CartDetails(UserID, CartHeaderID, ProductID, Quantity, UnitPrice, TotalPrice, DateAdded)" +
                "VALUES('" + cartDetails.UserID + "', '" + cartDetails.CartHeaderID + "', '" + cartDetails.ProductID + "', '" 
                            + cartDetails.Quantity + "', '" + cartDetails.UnitPrice + "', '" + cartDetails.TotalPrice + "', '" + dateAdded + "')";

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

        public static async Task<bool> CartDetailExists(CartDetailsModel cartDetails)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportExist = "SELECT * from CartDetails WHERE CartDetailID = '" + cartDetails.CartDetailID + "'";
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

        public static async Task<bool> CartDetailExistToVoid(CartDetailsModel cartDetails)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var isReportExist = "SELECT * from CartDetails WHERE CartHeaderID = '" + cartDetails.CartHeaderID + "'";
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


        public static async Task<List<CartDetailsModel>> GetCartDetails(CartDetailsModel cartDetail)
        {
            List<CartDetailsModel> cartDetails = new List<CartDetailsModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM CartDetails WHERE CartHeaderID ='" + cartDetail.CartHeaderID + "'";

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
                            CartDetailsModel cartDetailItem = new CartDetailsModel();
                            cartDetailItem.CartDetailID = reader.GetInt32(0);
                            cartDetailItem.UserID = reader.GetInt32(1);
                            cartDetailItem.CartHeaderID = reader.GetInt32(2);
                            cartDetailItem.ProductID = reader.GetInt32(3);
                            cartDetailItem.Quantity = reader.GetDecimal(4);
                            cartDetailItem.UnitPrice = reader.GetDecimal(5);
                            cartDetailItem.TotalPrice = reader.GetDecimal(6);
                            cartDetailItem.DateAdded = reader.GetDateTime(7);
                            cartDetails.Add(cartDetailItem);
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

                return cartDetails;
            }
        }//end of display method


        public static async Task<bool> DeleteCartDetail(CartDetailsModel cartDetails)
        {
            String SQL = "DELETE from CartDetails WHERE CartDetailID = '" + cartDetails.CartDetailID + "'";

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


        public static async Task<bool> VoidBill(CartDetailsModel cartDetails)
        {
            List<CartDetailsModel> cartDetailsToDelete = new List<CartDetailsModel>();
            var query = "SELECT * FROM CartDetails WHERE CartHeaderID ='" + cartDetails.CartHeaderID + "'";

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
                            cartDetailsToDelete.Add(cartDetailItem);
                        }
                    }
                    dbConn.Close();


                    foreach (var item in cartDetailsToDelete)
                    {
                        dbConn.Open();
                        String SQL = "DELETE from CartDetails WHERE CartDetailID = '" + item.CartDetailID + "'";
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = SQL;
                        cmd2.Connection = dbConn;
                        await cmd2.ExecuteNonQueryAsync();
                        dbConn.Close();
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