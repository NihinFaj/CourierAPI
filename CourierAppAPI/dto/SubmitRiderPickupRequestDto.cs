using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class SubmitRiderPickupRequestDto
    {
        public string QrCode { get; set; }
        public string RiderName { get; set; }
    }
}