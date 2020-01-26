using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Fee
{
    public class FeeModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
    }
}