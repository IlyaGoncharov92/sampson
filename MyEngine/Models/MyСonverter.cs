using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEngine.Models
{
    public class MyСonverter : Controller
    {
        UserContext db = new UserContext();

        public string ValueSpaceConvert(int value)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = " ";

            return value.ToString("#,#",nfi);
        }
    }
}