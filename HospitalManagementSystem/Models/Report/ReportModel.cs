using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ReportModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int ReportType { get; set; }
        public string Results { get; set; }
        public DateTime SampleDate { get; set; }
        public DateTime TestedDate { get; set; }
        public string Remarks { get; set; }
        public decimal Fee { get; set; }
        public string ReportHtml { get; set; }
    }
}