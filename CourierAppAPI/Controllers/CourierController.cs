using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CourierAppAPI.services;
using CourierAppAPI.dto;
using Newtonsoft.Json;

namespace CourierAppAPI.Controllers
{
    public class CourierController : ApiController
    {
        private CourierService courierService;

        public CourierController()
        {
            courierService = new CourierService();
        }

        [ActionName("get-all-names")]
        [HttpPost]
        public HttpResponseMessage GetNames()
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(errorList) };
            }
            var resp = courierService.GetListOfAllNames();

            if (resp == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "" };
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp, "application/json");
        }

        [ActionName("get-all-request")]
        [HttpPost]
        public HttpResponseMessage GetRequest(GetRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(errorList) };
            }
            var resp = courierService.GetAllRequest(dto);

            if (resp == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "" };
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp, "application/json");
        }

        [ActionName("register-user")]
        [HttpPost]
        public HttpResponseMessage RegisterUser()
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(errorList) };
            }
            var resp = courierService.RegisterUser();

            if (resp == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "" };
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp, "application/json");
        }

    }
}
