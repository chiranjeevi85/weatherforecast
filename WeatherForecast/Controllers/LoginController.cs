using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeatherForecast.Enums;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            LoginModel vModel = new LoginModel();
            HttpCookie cookie = Request.Cookies["Login"];

            if (cookie != null)
            {
                vModel.UserName = cookie.Value.Split(' ')[0];
                vModel.Password = cookie.Value.Split(' ')[1];
                vModel.CookiePersistence = true;
            }
            return View("~/Views/Login/Index.cshtml", vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginTheUser(LoginModel vModel)
        {
            try
            {
                if (FormsAuthentication.Authenticate(vModel.UserName, vModel.Password))
                {
                    Session["username"] = vModel.UserName;
                    FormsAuthentication.SetAuthCookie(vModel.UserName, false);
                    if (vModel.CookiePersistence)
                    {
                        HttpCookie cookie = new HttpCookie("Login", vModel.UserName + " " + vModel.Password);
                        Response.SetCookie(cookie);
                    }
                    else
                    {
                        if (System.Web.HttpContext.Current.Request.Cookies["Login"] != null)
                        {
                            HttpCookie currentUserCookie = System.Web.HttpContext.Current.Request.Cookies["Login"];
                            System.Web.HttpContext.Current.Response.Cookies.Remove("currentUser");
                            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                            currentUserCookie.Value = null;
                            System.Web.HttpContext.Current.Response.SetCookie(currentUserCookie);
                        }
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["username"] = null;
                    return Json("Invalid Login Details" + "¬" + (int)MessageType.Error, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message + "¬" + (int)MessageType.Error, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult LogoutTheUser()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return Json("/Login" + "¬" + (int)MessageType.Success, JsonRequestBehavior.AllowGet);
        }
    }
}