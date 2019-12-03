using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherApp.Models
{
    public class CurrentWeather
    {
        public City City { set; get; }
        public List<WeatherInfo> Weather { set; get; }
        public TemperatureInfo Main { set; get; }

        [JsonIgnore]
        public string Description => Weather[0].Description;

        public string IconPng
        {
            get
            {
                string icon_name = Weather[0].Icon;

                if (!(icon_name.Contains("d") || icon_name == "01n" || icon_name == "02n"))
                    icon_name = icon_name.Replace('n', 'd');
                if (icon_name == "04d")
                    icon_name = "03d";
                return $"icon{icon_name}.png";
            }

        }

        public string Dt_txt { set; get; }
        [JsonIgnore]
        public string Time
        {
            get
            {
                string[] date = Dt_txt.Split(new char[] { ' ', '-', ':', '.' });
                return $"{date[3]}:{date[4]}";
            }
        }
    }
}
