using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Services.BaseService
{
    public class BaseAppTenant
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["maindb"].ConnectionString;
    }
}