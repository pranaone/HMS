using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class TreatmentModel
    {
        public int ID { get; set; }
        public int AdmissionID { get; set; }
        public string PtntTreatment { get; set; }
        public string Medicines { get; set; }
        public string Reports { get; set; }
    }
}