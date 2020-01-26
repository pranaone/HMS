using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class AdmissionTreatmentModel
    {
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        public DateTime admissiondate {get; set; }
        public string PatientName { get; set; }
        public string RoomName { get; set; }
        public decimal RoomPrice { get; set; }
        public int RoomID { get; set; }
        public string PtntTreatment { get; set; }
        public string Medicines { get; set; }
        public string Reports { get; set; }
    }
}