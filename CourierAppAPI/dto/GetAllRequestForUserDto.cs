using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class GetAllRequestForUserDto
    {
        public string Delivery_Type { get; set; }
        public string Initiating_Officer { get; set; }
        public string Initiating_Officer_Email { get; set; }
        public DateTime? Initiating_Date { get; set; }
        public string PickUp_Branch { get; set; }
        public string Item_Type { get; set; }
        public int? Number_Of_Items { get; set; }
        public string QR_Code { get; set; }
        public string Barcode_Number { get; set; }
        public string Dispatch_Rider_Name { get; set; }
        public DateTime? Dispatch_Rider_Pickup_DateTime { get; set; }
        public string Item_Delivered_To { get; set; }
        public string Central_Mailroom_Officer { get; set; }
        public DateTime? Central_Mailroom_Date { get; set; }
        public string Delivery_Branch { get; set; }
        public DateTime? Delivery_Time_Date { get; set; }
        public string Delivery_Status { get; set; }
    }
}