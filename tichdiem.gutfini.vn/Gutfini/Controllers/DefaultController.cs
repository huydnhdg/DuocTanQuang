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
using System.Text;

namespace BigBB.Controllers
{
    public class DefaultController : Controller
    {
        public string LINK_API = "http://sms.bluesea.vn:8080/SmsPortal/api_tanquang.jsp";
        public string PRIVATE_KEY = "TANQUANG_gutfini@)@!";
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
                    else if (commandcode.Equals("kiemtra"))//truong hop tra cuu code se duoc de trong
                    {
                        RootObject jsonResult = CallRequest(username, commandcode, "");
                        msg = jsonResult.Result.message;
                    }
                    else if (commandcode.Equals("doiqua"))
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
        public RootObject CallRequest(string username, string commandcode, string code)
        {
            string category = "GUT";
            // String tempKey = Utilities.MD5(msisdn + code + commandcode + category + PrivateKey);
            String tempKey = CreateMD5(username + code + commandcode + category + PRIVATE_KEY);
            //Console.Write(tempKey);
            //Console.Write(
            // LINK_API + "?commandcode=" + commandcode + "&msisdn=" + username + "&code=" + code + "&category=" + category + "&key=" + tempKey);
            WebRequest request = WebRequest.Create(
            LINK_API+ "?commandcode=" + commandcode + "&msisdn=" + username + "&code=" + code + "&category=" + category + "&key=" + tempKey);
            

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

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
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