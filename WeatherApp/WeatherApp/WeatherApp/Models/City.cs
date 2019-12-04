using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WeatherApp.Infrastructure;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    public class City : Notifier
    {
        public string Name { set; get; }
        public string Country { set; get; }
        public int Id { set; get; }

        public City(IMainViewModel mainView)
        {
            MainViewModel = mainView;
        }

        public City() { }

        [JsonIgnore]
        private bool showRemoveButton;
        [JsonIgnore]
        public bool ShowRemoveButton
        {
            set
            {
                showRemoveButton = value;
                Notify();
            }
            get => showRemoveButton;
        }

        [JsonIgnore]
        public IMainViewModel MainViewModel { set; get; }

        [JsonIgnore]
        public ICommand RemoveCityCommand => new Command(() =>
        {
            MainViewModel.SelectedCityToRemove = this;
        });

    }
}
