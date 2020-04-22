using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherForecast.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool CookiePersistence { get; set; }

        //Info
        public int MessageType { get; set; }
        public string SuccessMessage { get; set; }
        public string InfoMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}