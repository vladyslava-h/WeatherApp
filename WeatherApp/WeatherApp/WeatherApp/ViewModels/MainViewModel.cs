using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class MainViewModel : Notifier
    {
        private NetworkManager networkManager;

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

        List<CurrentWeather> forecast;
        public List<CurrentWeather> Forecast
        {
            set
            {
                forecast = value;
                Notify();
            }
            get => forecast;
        }
        public MainViewModel()
        {
            networkManager = new NetworkManager();
            //CurrentWeather = new CurrentWeather();

            Task.Run(() =>
            {
                CurrentWeather = networkManager.GetCurrentWeather("Kyiv");
                Forecast = networkManager.GetForecast("Kiev").List;
            });
        }

    }
}
