using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Pharmacy
{
    public class MedicineModel
    {
        public long Mid { get; set; }
        public long Pid { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}