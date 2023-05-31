using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BigBB.Utils
{
    public static class StringExtension
    {
        public static string FormatPhonenumberStartWith84(this string PhoneNumber)
        {
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                return PhoneNumber;
            }
            PhoneNumber = Regex.Replace(PhoneNumber, "^0", "84");
            if (!PhoneNumber.StartsWith("84"))
            {
                PhoneNumber = "84" + PhoneNumber;
            }
            return PhoneNumber;
        }

        public static void WriteLogError(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + ex.Source + "; " + ex.Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }
        public static void WriteLogError(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }
    }
}