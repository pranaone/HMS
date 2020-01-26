using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Addmission
{
    public class WardModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DocName { get; set; }
        public int DoctorID { get; set; }
    }
}