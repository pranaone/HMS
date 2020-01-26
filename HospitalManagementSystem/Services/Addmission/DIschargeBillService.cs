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
    public class DIschargeBillService: BaseAppTenant
    {

        public static async Task<bool> AddDischargeBill(DischargeBillModel disbill)
        {
            DateTime billeddate = DateTime.Now;
            String SQL = "INSERT INTO AdmissionBill(AdmissionID, MedBill, ReportBill, RoomCharge, Date)" +
                "VALUES('" + disbill.AdmissionID + "','" + disbill.MedBill + "','" + disbill.ReportBill + "','" + disbill.RoomBill + "', @billDate)";
            //String SQL = "INSERT INTO AdmissionBill(AdmissionID, MedBill, ReportBill, RoomCharge, TotalBill, Date)" +
            //    "VALUES('" + disbill.AdmissionID + "','" + disbill.MedBill + "','" + disbill.ReportBill + "','" + disbill.RoomBill + "','" + disbill.TotalBill + "','" + disbill.BilledDate + "')";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Connection = dbConn;
                    cmd.Parameters.AddWithValue("@billDate", billeddate);
                    await cmd.ExecuteNonQueryAsync();
                    dbConn.Close();
                    await AddmissionService.UpdateDisDateRoomStatus(disbill);//update the room status and discharge date
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
    }
}