using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class AllUserDto
    {
        public string NameOfCourier { get; set; }
        public string UnitOrBranch { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}