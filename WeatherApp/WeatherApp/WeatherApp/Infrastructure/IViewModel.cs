using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.Infrastructure
{
    interface IViewModel
    {
        CurrentWeather CurrentWeather { set; get; }
        bool IsDayPage { get; }
        bool IsWeekPage { get; }
    }
}
