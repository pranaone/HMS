using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Pharmacy
{
    public class CartDetailsModel
    {
        public int CartDetailID { get; set; }
        public int UserID { get; set; }
        public int CartHeaderID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateAdded { get; set; }
    }
}