using BigBB.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace BigBB.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CallApi(string username, string pass, string commandcode)
        {

            string msg = "Chưa có yêu cầu nào được gửi đi.";

            try
            {
                if (username.Length == 0)
                {
                    msg = "Vui lòng nhập đủ thông tin.";
                }
                else
                {//truong hop tich diem pass duoc dung de thay code, pass de trong
                    username = username.FormatPhonenumberStartWith84();//start phone with 84
                    if (commandcode.Equals("tichdiem"))
                    {
                        RootObject jsonResult = CallRequest(username, commandcode, pass);
                        msg = jsonResult.Result.message;
                    }
                    else if(commandcode.Equals("kiemtra"))//truong hop tra cuu code se duoc de trong
                    {
                        RootObject jsonResult = CallRequest(username, commandcode, "");
                        msg = jsonResult.Result.message;
                    }
                    else if(commandcode.Equals("doiqua"))
                    {
                        RootObject jsonResult = CallRequest(username, commandcode, "");
                        msg = jsonResult.Result.message;
                    }
                }
                return Json(new
                {
                    success = true,
                    message = msg,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Hệ thống xảy ra lỗi.",
                }, JsonRequestBehavior.AllowGet);
            }

        }
        //http://sms.bluesea.vn:8000/SmsPortal/tanquangapi.jsp?commandcode=kiemtra&msisdn=84912573734&category=VGK
        public RootObject CallRequest(string username, string commandcode, string code)
        {
            string category = "VDGK";
            WebRequest request = WebRequest.Create(
             "http://sms.bluesea.vn:8080/SmsPortal/tanquangapi.jsp?commandcode=" + commandcode + "&msisdn=" + username + "&code=" + code + "&category=" + category);
            
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //xữ lý dữ liệu ở đây
            JavaScriptSerializer jss = new JavaScriptSerializer();
            // convert json string
            RootObject result = JsonConvert.DeserializeObject<RootObject>(responseFromServer);
            reader.Close();
            response.Close();
            return result;
        }
    }
    public class Result
    {
        public string message { get; set; }
        public string point { get; set; }
        public string status { get; set; }
        public string msisdn { get; set; }
        public string code { get; set; }
    }

    public class RootObject
    {
        public Result Result { get; set; }
    }

}