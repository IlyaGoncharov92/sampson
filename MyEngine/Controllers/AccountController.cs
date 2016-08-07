using System;
using System.Web;
using System.Web.Mvc;
using MyEngine.Models;
using System.Web.Security;
using MyEngine.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;


namespace MyEngine.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckAuthorize()
        {
            string urlAuthorize = "";

            if(Request.IsAuthenticated)
            {
                urlAuthorize = "/Account/LoginMenu";
            }
            else
            {
                urlAuthorize = "/Account/Login";
            }

            return Json(urlAuthorize, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginMenu()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        /*-------------------------*/

        public ActionResult Login()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Login");
            }
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (Request.IsAjaxRequest())
                        {
                            return Json("");
                        }
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return Json("");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult Register()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    if (Request.IsAjaxRequest())
                    {
                        return Json("");
                    }
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View(model);
        }
        /*-----------------------------------------------------------------------*/
    }
}
