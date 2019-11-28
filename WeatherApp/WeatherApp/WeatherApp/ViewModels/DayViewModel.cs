using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherApp.Infrastructure;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class DayViewModel : Notifier, IViewModel
    {
        private CurrentWeather currentWeather;
        public CurrentWeather CurrentWeather
        {
            set
            {
                currentWeather = value;
                Notify();
            }
            get => currentWeather;
        }
        public ObservableCollection<WeekdayWeather> WeekdaysWeather { set; get; }
        public bool IsDayPage => true;

        public bool IsWeekPage => false;

        public void Init(object cw)
        {
            this.CurrentWeather = cw as CurrentWeather;
        }
    }
}
