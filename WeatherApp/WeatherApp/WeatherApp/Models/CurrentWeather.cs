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

    }
}
