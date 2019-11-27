using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Infrastructure;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class WeekViewModel : IViewModel
    {
        public CurrentWeather CurrentWeather { get; set; }

        public bool IsDayPage => false;

        public bool IsWeekPage => true;


    }
}
