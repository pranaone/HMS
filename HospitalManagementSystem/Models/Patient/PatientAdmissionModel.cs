using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Patient
{
    public class PatientAdmissionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime AdmittedDate { get; set; }
        public decimal RoomPrice { get; set; }
    }
}