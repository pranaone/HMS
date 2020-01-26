using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class InPatientModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomStatus { get; set; }
        public DateTime DateAddmitted { get; set; }
        //public Decimal AdmissionFee { get; set; }
        public DateTime DateDischarged { get; set; }
    }
}