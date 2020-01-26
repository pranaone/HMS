using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.ViewReports
{
    public class SalesReportModel
    {
        public int ID { get; set; }
        //public int CartHeaderID { get; set; }
        public int PatientID { get; set; }
        public int ProductID { get; set; }
        public string PatientName { get; set; }
        public decimal TotalPrice { get; set; }
        //public decimal TaxAmount { get; set; }
        public decimal TotalBill { get; set; }
        public DateTime SalesDate { get; set; }
        public string SearchFromDate { get; set; }
        public string SearchToDate { get; set; }

    }
}