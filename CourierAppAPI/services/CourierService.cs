using CourierAppAPI.Context;
using CourierAppAPI.dto;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

        public ResponseDto GetListOfAllNames()
        {
            try
            {
                Logger.Info("Just entered GetNames function");

                var ResponseDto = new ResponseDto();

                Logger.Info("About to call GetAllNames function");

                var resp = GetAllNames();

                if (resp == null)
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Message = "";
                    ResponseDto.Error = "Unable to process request, please try again later.";
                    return ResponseDto;
                }

                ResponseDto.StatusCode = 1000;
                ResponseDto.Message = JsonConvert.SerializeObject(resp);
                ResponseDto.Error = "";
                return ResponseDto;
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
                    var list = db.ExecuteQuery<AllUserDto>("select Courier_Name, Units_Branches, Phone_Numbers, Email_Address,Status, Branch_Code from Dispatch_Riders_Lagos", new Object[] { }).ToList();
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

        public ResponseDto GetAllRiderRequests(GetRiderRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered GetAllRiderRequests function");

                var ResponseDto = new ResponseDto();

                Logger.Info("About to call GetRiderRequestList function");

                var resp = GetRiderRequestList(dto);

                if (resp == null)
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Message = "";
                    ResponseDto.Error = "Unable to get requests, please try again later.";
                    return ResponseDto;
                }

                Logger.Info("Response from GetRiderRequestList function is: " + JsonConvert.SerializeObject(resp));

                ResponseDto.StatusCode = 1000;
                ResponseDto.Message = JsonConvert.SerializeObject(resp);
                ResponseDto.Error = "";
                return ResponseDto;
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllRiderRequests function entered an exception");
                Logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<GetAllRequestForUserDto> GetRiderRequestList(GetRiderRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered GetRiderRequestList function");

                using (var db = new EOneContext())
                {
                    var list = db.ExecuteQuery<GetAllRequestForUserDto>("select * from Courier_Tracker where Dispatch_Rider_Name is Null and Dispatch_Rider_Pickup_DateTime is Null ", new Object[] { }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetRiderRequestList function entered an exception");
                Logger.Error(ex);
                return null;
            }

        }

        public ResponseDto GetAllMailroomRequests(GetMailroomRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered GetAllMailroomRequests function");

                var ResponseDto = new ResponseDto();

                Logger.Info("About to call GetMailroomRequestList function");

                var resp = GetMailroomRequestList(dto);

                if (resp == null)
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Message = "";
                    ResponseDto.Error = "Unable to process request, please try again later.";
                    return ResponseDto;
                }

                Logger.Info("Response from GetMailroomRequestList function is: " + JsonConvert.SerializeObject(resp));

                ResponseDto.StatusCode = 1000;
                ResponseDto.Message = JsonConvert.SerializeObject(resp);
                ResponseDto.Error = "";
                return ResponseDto;
            }
            catch (Exception ex)
            {
                Logger.Error("GetMailroomRequestList function entered an exception");
                Logger.Error(ex);
                return null;
            }

        }

        public IEnumerable<GetAllRequestForUserDto> GetMailroomRequestList(GetMailroomRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered GetMailroomRequestList function");

                using (var db = new EOneContext())
                {
                    var list = db.ExecuteQuery<GetAllRequestForUserDto>("select * from Courier_Tracker where Dispatch_Rider_Name is not Null and Dispatch_Rider_Pickup_DateTime is not Null and Central_Mailroom_Officer is Null and Central_Mailroom_Date is Null", new Object[] { }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetMailroomRequestList function entered an exception");
                Logger.Error(ex);
                return null;
            }

        }

        public ResponseDto RegisterUser(RegisterUserDto dto)
        {
            try
            {
                Logger.Info("Just entered RegisterUser Function " + dto.Email);
                var ResponseDto = new ResponseDto();

                Logger.Info("About to call RegisterCurrentUser Function " + dto.Email);
                var resp = RegisterCurrentUser(dto);

                if (resp > 0)
                {
                    ResponseDto.StatusCode = 1000;
                    ResponseDto.Error = "";
                    ResponseDto.Message = "Rider was registered successfully";
                    return ResponseDto;
                }
                else
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Error = "Sorry, User could not be registered at the moment";
                    ResponseDto.Message = "";
                    return ResponseDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info("RegisterUser function entered an exception " + dto.Email);
                return null;
            }
        }

        public Int64 RegisterCurrentUser(RegisterUserDto dto)
        {
            try
            {
                Logger.Info("Just entered RegisterCurrentUser function " + dto.Email);

                var dateRegistered = DateTime.Now.ToString();

                using (var db = new EOneContext())
                {
                    Int64 a = 0;
                    Logger.Info("***About to call stored procedure dbo.register_dispatch_rider, with values Email Address " + dto.Email + " Device Id " + dto.DeviceId + " Status " + dto.Status);
                    var cmd = db.Database.Connection.CreateCommand();
                    cmd.CommandText = "dbo.register_dispatch_rider";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", dto.Email.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@DeviceId", dto.DeviceId.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@RegisterStatus", dto.Status.Trim()));

                    try
                    {
                        Logger.Info("Opening the connection " + dto.Email);

                        ((IObjectContextAdapter)db).ObjectContext.Connection.Open();

                        a = Convert.ToInt64(cmd.ExecuteNonQuery());
                        Logger.Info(dto.Email + " Rider was successfully registered, Number of Rows Affected is " + a);
                    }
                    catch (Exception ex)
                    {
                        Logger.Info("Entered an exeption code block when calling stored procedure dbo.register_dispatch_rider " + dto.Email);
                        Logger.Error(ex);
                        return -1;
                    }
                    finally
                    {
                        Logger.Info("Closing the connection to the EOne DB after insertion " + dto.Email);
                        db.Database.Connection.Close();
                    }
                    Logger.Info("About to return Id afer registering Dispatch Rider " + dto.Email);

                    return a;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllRequestForUser function entered an exception " + dto.Email);
                Logger.Error(ex);
                return -1;
            }
        }

        public ResponseDto SubmitRiderPickUp(SubmitRiderPickupRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered SubmitRiderPickUp Function " + dto.RiderName);
                var ResponseDto = new ResponseDto();

                Logger.Info("About to call SubmitRiderPickUpOnDB Function " + dto.RiderName);
                var resp = SubmitRiderPickUpOnDB(dto);

                if (resp > 0)
                {
                    ResponseDto.StatusCode = 1000;
                    ResponseDto.Error = "";
                    ResponseDto.Message = "Rider pickup request was submitted successfully";
                    return ResponseDto;
                }
                else
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Error = "Sorry, Rider pickup request could not be submitted at the moment";
                    ResponseDto.Message = "";
                    return ResponseDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info("SubmitRiderPickUp function entered an exception " + dto.RiderName);
                return null;
            }
        }

        public Int64 SubmitRiderPickUpOnDB(SubmitRiderPickupRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered SubmitRiderPickUpOnDB function " + dto.RiderName);

                var dateRegistered = DateTime.Now.ToString();

                using (var db = new EOneContext())
                {
                    Int64 a = 0;
                    Logger.Info("***About to call stored procedure dbo.submit_rider_pickup_request, with values Email Address " + dto.RiderName + " QRCode " + dto.QrCode);
                    var cmd = db.Database.Connection.CreateCommand();
                    cmd.CommandText = "dbo.submit_rider_pickup_request";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@qrCode", dto.QrCode.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@riderName", dto.RiderName.Trim()));

                    try
                    {
                        Logger.Info("Opening the connection, for rider" + dto.RiderName);

                        ((IObjectContextAdapter)db).ObjectContext.Connection.Open();

                        a = Convert.ToInt64(cmd.ExecuteNonQuery());
                        Logger.Info(dto.RiderName + " Rider pickup request was successfully submitted, Number of Rows Affected is " + a);
                    }
                    catch (Exception ex)
                    {
                        Logger.Info("Entered an exeption code block when calling stored procedure dbo.submit_rider_pickup_request " + dto.RiderName);
                        Logger.Error(ex);
                        return -1;
                    }
                    finally
                    {
                        Logger.Info("Closing the connection to the EOne DB after insertion " + dto.RiderName);
                        db.Database.Connection.Close();
                    }
                    Logger.Info("About to return Id afer submitting Rider pickup request " + dto.RiderName);

                    return a;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("SubmitRiderPickUpOnDB function entered an exception " + dto.RiderName);
                Logger.Error(ex);
                return -1;
            }
        }

        public ResponseDto SubmitMailroomPickUp(SubmitMailroomPickupRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered SubmitMailroomPickUp Function " + dto.MailRoomName);
                var ResponseDto = new ResponseDto();

                Logger.Info("About to call SubmitMailroomPickUpOnDB Function " + dto.MailRoomName);
                var resp = SubmitMailroomPickUpOnDB(dto);

                if (resp > 0)
                {
                    ResponseDto.StatusCode = 1000;
                    ResponseDto.Error = "";
                    ResponseDto.Message = "Mailroom pickup request was submitted successfully";
                    return ResponseDto;
                }
                else
                {
                    ResponseDto.StatusCode = 1001;
                    ResponseDto.Error = "Sorry, Mailroom pickup request could not be submitted at the moment";
                    ResponseDto.Message = "";
                    return ResponseDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info("SubmitMailroomPickUp function entered an exception " + dto.MailRoomName);
                return null;
            }
        }

        public Int64 SubmitMailroomPickUpOnDB(SubmitMailroomPickupRequestDto dto)
        {
            try
            {
                Logger.Info("Just entered SubmitMailroomPickUpOnDB function " + dto.MailRoomName);

                var dateRegistered = DateTime.Now.ToString();

                using (var db = new EOneContext())
                {
                    Int64 a = 0;
                    Logger.Info("***About to call stored procedure dbo.submit_mailroom_pickup_request, with values Email Address " + dto.MailRoomName + " QRCode " + dto.QrCode);
                    var cmd = db.Database.Connection.CreateCommand();
                    cmd.CommandText = "dbo.submit_mailroom_pickup_request";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@qrCode", dto.QrCode.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@mailRoomName", dto.MailRoomName.Trim()));

                    try
                    {
                        Logger.Info("Opening the connection, for mailroom officer" + dto.MailRoomName);

                        ((IObjectContextAdapter)db).ObjectContext.Connection.Open();

                        a = Convert.ToInt64(cmd.ExecuteNonQuery());
                        Logger.Info(dto.MailRoomName + " Mailroom pickup request was successfully submitted, Number of Rows Affected is " + a);
                    }
                    catch (Exception ex)
                    {
                        Logger.Info("Entered an exeption code block when calling stored procedure dbo.submit_mailroom_pickup_request " + dto.MailRoomName);
                        Logger.Error(ex);
                        return -1;
                    }
                    finally
                    {
                        Logger.Info("Closing the connection to the EOne DB after insertion " + dto.MailRoomName);
                        db.Database.Connection.Close();
                    }
                    Logger.Info("About to return Id afer submitting Mailroom officer pickup request " + dto.MailRoomName);

                    return a;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("SubmitMailroomPickUpOnDB function entered an exception " + dto.MailRoomName);
                Logger.Error(ex);
                return -1;
            }
        }
    }
}