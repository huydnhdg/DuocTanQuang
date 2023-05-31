using System;
using System.Collections.Generic;
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
    }
}