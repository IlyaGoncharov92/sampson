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

        //метод предназначен для преобразования стоимости в объявлениях к боллее читабельному виду
        public string ValueSpaceConvert(int value)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = " ";

            return value.ToString("#,#",nfi);
        }
        /*---------------------------------------------------------------------------------*/

        public string UserValidation()
        {   

            //string name = HttpContext.User.Identity.Name;



            string l = db.Roles.Find(2).Name;

            return l;
        }
    }
}