using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Pharmacy
{
    public class CartHeaderModel
    {
        public int CartHeaderID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}