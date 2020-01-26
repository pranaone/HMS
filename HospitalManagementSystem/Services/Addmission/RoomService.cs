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
    public class RoomService : BaseAppTenant
    {
        public static async Task<List<RoomModel>> GetRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * from Room";
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
                            RoomModel roomItems = new RoomModel();
                            roomItems.ID = reader.GetInt32(0);
                            roomItems.Name = reader.GetString(1);
                            roomItems.Price = reader.GetDecimal(2);
                            roomItems.IsAvailable = reader.GetBoolean(3);
                            roomItems.WardID = reader.GetInt32(4);
                            rooms.Add(roomItems);
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

                return rooms;
            }
        }


        public static async Task<List<RoomModel>> GetAvailableRoomsByWard(RoomModel room)
        {
            List<RoomModel> rooms = new List<RoomModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Room WHERE WardID = '" + room.WardID + "' AND IsAvailable = 1";
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
                            RoomModel roomItems = new RoomModel();
                            roomItems.ID = reader.GetInt32(0);
                            roomItems.Name = reader.GetString(1);
                            //roomItems.Price = reader.GetDecimal(2);
                            //roomItems.IsAvailable = reader.GetBoolean(3);
                            //roomItems.WardID = reader.GetInt32(2);
                            rooms.Add(roomItems);
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

                return rooms;
            }
        }

        public static async Task<List<RoomModel>> GetRoomPrice(RoomModel room)
        {
            List<RoomModel> rooms = new List<RoomModel>();
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Room WHERE ID = '"+ room.ID +"'";
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
                            RoomModel roomItems = new RoomModel();
                            //roomItems.ID = reader.GetInt32(0);
                            //roomItems.Name = reader.GetString(1);
                            roomItems.Price = reader.GetDecimal(2);
                            //roomItems.IsAvailable = reader.GetBoolean(3);
                            //roomItems.WardID = reader.GetInt32(2);
                            rooms.Add(roomItems);
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

                return rooms;
            }
        }


        public static async Task<bool> UpdateRoomAvailability(int roomid)
        {
            String SQL = "UPDATE Room SET IsAvailable = 0 WHERE ID =" + roomid;
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


        public static async Task<bool> AddRoom(RoomModel room)
        {
            String SQL = "INSERT INTO Room(Name,Price,IsAvailable,WardID) VALUES(@Name,@Price,@IsAvail,@WardID)";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@Name", room.Name);
                    cmd.Parameters.AddWithValue("@Price", room.Price);
                    cmd.Parameters.AddWithValue("@IsAvail", room.IsAvailable);
                    cmd.Parameters.AddWithValue("@WardID", room.WardID);

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

        public static async Task<bool> UpdateRoom(RoomModel room)
        {
            String SQL = "UPDATE Room SET Name = @Name, Price = @Price WHERE ID = @ID";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@ID", room.ID);
                    cmd.Parameters.AddWithValue("@Name", room.Name);
                    cmd.Parameters.AddWithValue("@Price", room.Price);
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


        public static async Task<bool> DeleteRoom(RoomModel room)
        {
            String SQL = "DELETE from Room WHERE ID = '" + room.ID + "'";

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