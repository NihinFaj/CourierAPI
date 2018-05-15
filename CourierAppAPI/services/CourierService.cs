using CourierAppAPI.Context;
using CourierAppAPI.dto;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CourierAppAPI.services
{
    public class CourierService
    {
        protected ILog Logger;
        public CourierService()
        {
            Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public string GetListOfAllNames()
        {
            try
            {
                Logger.Info("Just entered GetNames function");

                var ResponseDto = new ResponseDto();

                Logger.Info("About to call GetAllNames function");

                var resp = GetAllNames();

                if (resp == null)
                {
                    ResponseDto.Code = "1001";
                    ResponseDto.Message = "";
                    ResponseDto.Error = "Unable to process request, please try again later.";
                    return JsonConvert.SerializeObject(ResponseDto);
                }

                Logger.Info("Response from GetAllNames function is: " + JsonConvert.SerializeObject(resp));
                
                ResponseDto.Code = "1000";
                ResponseDto.Message = JsonConvert.SerializeObject(resp);
                ResponseDto.Error = "";
                return JsonConvert.SerializeObject(ResponseDto);
            }
            catch (Exception ex)
            {
                Logger.Error("GetNames function entered an exception");
                Logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<AllUserDto> GetAllNames()
        {
            try
            {
                Logger.Info("Just entered GetAllNames function");

                using (var db = new EOneContext())
                {
                    var list = db.ExecuteQuery<AllUserDto>("select Courier_Name, Units_Branches, Phone_Numbers, Email_Address from Dispatch_Riders_Lagos", new Object[] { }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllNames function entered an exception");
                Logger.Error(ex);
                return null;
            }

        }

        public ResponseDto RegisterUser()
        {
            try
            {
                var resp = new ResponseDto();
                resp.Code = "1000";
                resp.Error = "";
                resp.Message = "Successful";
                return resp;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
    }
}