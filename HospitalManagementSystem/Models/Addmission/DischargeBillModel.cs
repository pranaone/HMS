using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class DischargeBillModel
    {
        public int AdmissionID { get; set; }
        public int RoomID { get; set; }
        public decimal MedBill { get; set; }
        public decimal ReportBill { get; set; }
        public decimal RoomBill { get; set; }
        //public decimal TotalBill { get; set; }
        //public DateTime BilledDate { get; set; }
        public DateTime DischargedDate { get; set; }


    }
}