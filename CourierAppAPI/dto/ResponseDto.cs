using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierAppAPI.dto
{
    public class ResponseDto
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }
    }
}