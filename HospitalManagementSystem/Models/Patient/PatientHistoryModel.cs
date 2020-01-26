using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Patient
{
    public class PatientHistoryModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Changes { get; set; }
        public string Remarks { get; set; }
        public string LabReport { get; set; }
        public string Prescription { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

    }
}