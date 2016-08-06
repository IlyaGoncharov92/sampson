using System.Collections.Generic;
using System.Linq;
using MyEngine.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyEngine.Controllers
{
    public class SectionController : Controller
    {
        public ActionResult Discount()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Services()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult History()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Partners()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Contacts()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
    }
}