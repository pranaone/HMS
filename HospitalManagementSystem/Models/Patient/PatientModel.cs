using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Patient
{
    public class PatientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public bool isNonNIC { get; set; }
        public string GuardianNIC { get; set; }

    }
}