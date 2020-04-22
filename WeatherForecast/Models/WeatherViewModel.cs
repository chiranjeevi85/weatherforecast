using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using static WeatherForecast.Models.WeatherModel;

namespace WeatherForecast.Models
{
    public class WeatherViewModel
    {
        public string WeatherDate { get; set; }
        public string WeatherState { get; set; }
        public string ImgUrl { get; set; }

        public List<WeatherViewModel> GenerateViewModel(string url)
        {
            var weatherviewmodel = new List<WeatherViewModel>();

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                //Converting to OBJECT from JSON string.
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                foreach (var obj in weatherInfo.consolidated_weather.Take(5))
                {
                    var weathermodel = new WeatherViewModel
                    {
                        WeatherDate = DateTime.ParseExact(obj.applicable_date, "yyyy-mm-dd", CultureInfo.InvariantCulture).ToString("ddd dd MMMM"),
                        WeatherState = obj.weather_state_name,
                        ImgUrl = "https://www.metaweather.com/static/img/weather/" + obj.weather_state_abbr + ".svg"
                    };
                    weatherviewmodel.Add(weathermodel);
                }

            }
            return weatherviewmodel;
        }
    }
}