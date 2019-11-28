using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherApp.Models
{
    class CurrentWeather
    {
        public string Name { set; get; }
        public List<WeatherInfo> Weather { set; get; }
        public TemperatureInfo Main { set; get; }

        public string Description => Weather[0].Description;
        public string ImageSource
        {
            get
            {
                if (Weather[0].Icon == "10n")
                    return "i09n.gif";
                if (Weather[0].Icon == "04n")
                    return "i04d.gif";
                return $"i{Weather[0].Icon}.gif";
            }
        }

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
