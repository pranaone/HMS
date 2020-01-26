using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Common
{
    public class CommonResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public bool TokenHasExpired { get; set; }
    }
}