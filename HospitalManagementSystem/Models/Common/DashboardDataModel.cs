using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Common
{
    public class DashboardDataModel
    {
        public int NoOfPatients { get; set; }
        public int NoOfPatientsAdmitted { get; set; }
        public int NoOfPatientsDischarged { get; set; }
        public int NoOfPatientsInHouse { get; set; }
    }
}