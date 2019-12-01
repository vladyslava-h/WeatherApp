using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    class WeatherViewModel : Notifier
    {
        private NetworkManager networkManager;

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

        public ObservableCollection<IViewModel> ViewModels { set; get; }

        private string backgroundSource;
        public string BackgroundSource
        {
            set
            {
                backgroundSource = value;
                Notify();
            }
            get => backgroundSource;
        }

        public WeatherViewModel(City city)
        {
            networkManager = new NetworkManager();
            ViewModels = new ObservableCollection<IViewModel>() { new DayViewModel(), new WeekViewModel() };

            Task.Run(() =>
            {
                CurrentWeather currentWeather = networkManager.GetCurrentWeather(city.Id);
                currentWeather.City = city;
                Forecast = networkManager.GetForecast(city.Id).List;
                BackgroundSource = currentWeather.ImageSource;

                Device.BeginInvokeOnMainThread(new Action(() =>
                {
                    ViewModels[0].Init(currentWeather);
                    ViewModels[1].Init(Forecast);
                }));
            });

        }

    }
}
