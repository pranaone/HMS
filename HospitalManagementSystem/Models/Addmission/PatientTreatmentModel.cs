using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class PatientTreatmentModel
    {
        public int AdmissionID { get; set; }
        public string PatientName { get; set; }
        public string RoomName { get; set; }

        public List<TreatmentModel> PtntTreatments { get; set; }
    }
}