using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Pharmacy
{
    public class InventoryModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}