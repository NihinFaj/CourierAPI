using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class RegisterUserDto
    {
        public string DeviceId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}