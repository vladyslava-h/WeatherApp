using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class MainViewModel : Notifier, IViewModel
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

        public List<IViewModel> ViewModels { set; get; }
        public bool IsDayPage => true;
        public bool IsWeekPage => false;

        private bool showForecast;
        public bool ShowForecast
        {
            set
            {
                showForecast = value;
                Notify();
            }
            get => showForecast;
        } 

        private IViewModel selectedPosition;
        public IViewModel SelectedPosition
        { 
            set
            {
                if (value != this)
                    ShowForecast = false;
                else
                    ShowForecast = true;
                selectedPosition = value;
            }
            get => selectedPosition;
        }

        public MainViewModel()
        {
            networkManager = new NetworkManager();
            ViewModels = new List<IViewModel>()
            {
                this,
                new WeekViewModel()
            };
            SelectedPosition = ViewModels[0];

            Task.Run(() =>
            {
                CurrentWeather = networkManager.GetCurrentWeather("Kyiv");
                Forecast = networkManager.GetForecast("Kiev").List;
            });
        }

    }
}
