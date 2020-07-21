using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class AllUserDto
    {
        public string Courier_Name { get; set; }
        public string Units_Branches { get; set; }
        public double Phone_Numbers { get; set; }
        public string Email_Address { get; set; }
        public string Status { get; set; }
        public string Branch_Code { get; set; }
    }
}