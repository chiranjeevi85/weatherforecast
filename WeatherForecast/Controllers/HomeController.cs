using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherForecast.Models;
using static WeatherForecast.Models.WeatherModel;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == true)
            {
                string url = string.Format("https://www.metaweather.com/api/location/44544/");

                var vm = new WeatherViewModel();
                var weatherviewmodel = vm.GenerateViewModel(url);

                return View(weatherviewmodel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}