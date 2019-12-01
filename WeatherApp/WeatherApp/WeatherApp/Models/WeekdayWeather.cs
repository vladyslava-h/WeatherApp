using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class WeekdayWeather
    {
        public string WeekdayName { set; get; } 
        public string Icon { set; get; }
        public int MaxTemp { set; get; }
        public int MinTemp { set; get; }
    }
}
