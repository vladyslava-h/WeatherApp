using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherApp.Infrastructure;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class WeekViewModel : Notifier, IViewModel
    {
        public CurrentWeather CurrentWeather { get; set; }

        public bool IsDayPage => false;

        public bool IsWeekPage => true;

        public ObservableCollection<WeekdayWeather> WeekdaysWeather { set; get; }

        public WeekViewModel()
        {
            WeekdaysWeather = new ObservableCollection<WeekdayWeather>();
        }

        public void Init(object forecast)
        {
            List<WeekdayWeather> tmp_list = new List<WeekdayWeather>();
            foreach (var item in forecast as List<CurrentWeather>)
            {
                WeekdayWeather tmp_weather = new WeekdayWeather()
                {
                    Icon = item.IconPng,
                    MaxTemp = item.Main.Temp_maxInt,
                    MinTemp = item.Main.Temp_minInt,
                };

                string[] date = item.Dt_txt.Split(new char[] { ' ', '-', ':', '.' });
                DateTime dateTime = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                tmp_weather.WeekdayName = dateTime.ToString("dddd");

                tmp_list.Add(tmp_weather);
            }
            SortData(tmp_list);
        }

        public void SortData(List<WeekdayWeather> tmp_list)
        {
            var list = tmp_list.GroupBy(x => x.WeekdayName);
            foreach (var item in list)
            {
                int max = item.OrderByDescending(x => x.MaxTemp).FirstOrDefault().MaxTemp;
                int min = item.OrderBy(x => x.MinTemp).FirstOrDefault().MinTemp;
                string name = item.Select(x => x.WeekdayName).FirstOrDefault();
                string icon = item.GroupBy(x => x.Icon).Select(x => new { Icon = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).Select(x => x.Icon).FirstOrDefault();

                WeekdaysWeather.Add(new WeekdayWeather() { Icon = icon, WeekdayName = name, MaxTemp = max, MinTemp = min });
            }
        }
    }

}
