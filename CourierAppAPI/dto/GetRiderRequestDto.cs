using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class GetRiderRequestDto
    {
        public string BranchCode { get; set; }
        public int RiderEmail { get; set; }
    }
}