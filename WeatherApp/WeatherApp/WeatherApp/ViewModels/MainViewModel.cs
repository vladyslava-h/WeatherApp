using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using WeatherApp.Models;
using WeatherApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    class MainViewModel : Notifier, IMainViewModel
    {
        IIOService<List<City>> iOService;
        private ObservableCollection<MasterPageItem> menuItems;
        public ObservableCollection<MasterPageItem> MenuItems
        {
            set => menuItems = value;
            get => menuItems ?? (menuItems = new ObservableCollection<MasterPageItem>());
        }

        private MasterPageItem selectedItem;
        public MasterPageItem SelectedItem
        {
            set
            {
                foreach (var item in MenuItems)
                {
                    if (item.Title == value.Title)
                        item.TextColor = Color.Black;
                    else
                        item.TextColor = Color.White;
                }
                selectedItem = value;
                Notify();
            }
            get => selectedItem;
        }

        public List<City> Cities { set; get; }

        public MainViewModel(IIOService<List<City>> service)
        {
            iOService = service;

            LoadCities();
            LoadMenuItems();
        }

        public async void LoadCities()
        {
            NetworkManager networkManager = new NetworkManager();
            Cities = await networkManager.GetCities();
        }

        public Task<City> FindCity(int id)
        {
            return Task.Factory.StartNew(() => Cities.Where(x => x.Id == id).First());
        }

        public void SaveCities()
        {
            List<City> cities = new List<City>();
            foreach (var item in MenuItems)
                cities.Add(((WeatherViewModel)item.View.BindingContext).ViewModels[0].CurrentWeather.City);
            iOService.Save(cities);
        }

        public void LoadMenuItems()
        {
            List<City> users_cities = iOService.Load();
            if (users_cities == null)
            {
                City city = new City() { Name = "Kyiv", Country = "UA", Id = 703448 };
                MenuItems.Add(new MasterPageItem() { Title = "Kiev", View = new WeatherView(city), TextColor = Color.White });
            }
            else
            {
                foreach (var city in users_cities)
                    MenuItems.Add(new MasterPageItem() { Title = city.Name, View = new WeatherView(city), TextColor = Color.White });
            }

        }
    }
}
