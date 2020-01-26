using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.User
{
    public class UserForUsersPage
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address_line_1 { get; set; }
        public string Address_line_2 { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string RoleID { get; set; }
        public string Doctor_Category { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string ActiveStatus { get; set; }
    }
}