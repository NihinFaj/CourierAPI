using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class SubmitMailroomPickupRequestDto
    {
        public string MailRoomName { get; set; }
        public string QrCode { get; set; }
    }
}