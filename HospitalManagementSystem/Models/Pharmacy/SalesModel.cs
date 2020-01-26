using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Pharmacy
{
    public class SalesModel
    {
        public int ID { get; set; }
        public int CartHeaderID { get; set; }
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public int TotalPrice { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalBill { get; set; }
        public DateTime SalesDate { get; set; }

    }
}