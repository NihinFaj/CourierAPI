using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CourierAppAPI.dto
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }


    }
}