using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            get => selectedItem ?? (selectedItem = MenuItems[0]);
        }

        public List<City> Cities { set; get; }

        private bool isListCityRefresh;
        public bool IsListCityRefresh
        {
            set
            {
                isListCityRefresh = value;
                Notify();
            }
            get => isListCityRefresh;
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Task.Run(() =>
            {
                bool success = false;
                IsListCityRefresh = true;
                SearchBarIsEnabled = ShowMenuList = false;
                ShowResultsList = true;
                while (success != true)
                {
                    try
                    {
                        SearchResults = Cities.Where(x => x.Name.StartsWith(query)).ToList();
                        IsListCityRefresh = false;
                        List<int> existed_cities = new List<int>();
                        foreach (var item in MenuItems)
                            existed_cities.Add(((WeatherViewModel)item.View.BindingContext).ViewModels[0].CurrentWeather.City.Id);

                        foreach (var res in searchResults)
                        {
                            if (existed_cities.Contains(res.Id))
                            {
                                res.ShowRemoveButton = true;
                                res.MainViewModel = this;
                            }
                            else res.ShowRemoveButton = false;

                        }

                        SearchBarIsEnabled = true;
                        success = true;
                    }
                    catch { }
                }

            });

        });

        private City selectedCity;
        public City SelectedCity
        {
            set
            {
                selectedCity = value;
                List<int> tmp_cities = new List<int>();
                try
                {
                    if (MenuItems != null && MenuItems.Count != 0)
                    {
                        foreach (var item in MenuItems)
                            tmp_cities.Add(((WeatherViewModel)item.View.BindingContext).ViewModels[0].CurrentWeather.City.Id);
                        if (!tmp_cities.Contains(SelectedCity.Id))
                        {
                            if (value != null && value.Name != string.Empty)
                            {
                                SelectedCity.MainViewModel = this;
                                MenuItems.Add(new MasterPageItem() { Title = SelectedCity.Name, View = new WeatherView(SelectedCity), TextColor = Color.White });
                                ShowMenuList = true;
                                ShowResultsList = false;
                            }
                        }
                    }
                    ShowMenuList = true;
                    ShowResultsList = false;
                    Notify();
                }
                catch { }
            }
            get => selectedCity ?? (SelectedCity = new City(this) { Name = string.Empty, Country = string.Empty, Id = 0 });
        }


        private City selectedCityToRemove;
        public City SelectedCityToRemove
        {
            set
            {
                selectedCityToRemove = value;
                if (value != null && value.Name != string.Empty && value.Id != ((WeatherViewModel)SelectedItem.View.BindingContext).ViewModels[0].CurrentWeather.City.Id)
                {
                    ShowMenuList = true;
                    ShowResultsList = false;

                    MenuItems.Remove(MenuItems.Where(x => ((WeatherViewModel)x.View.BindingContext)
                    .ViewModels[0].CurrentWeather.City.Id == selectedCityToRemove.Id).FirstOrDefault());
                }

                Notify();
            }
            get => selectedCityToRemove ?? (SelectedCityToRemove = new City(this) { Name = string.Empty, Id = 0, Country = string.Empty });
        }

        private bool showResultsList;
        public bool ShowResultsList
        {
            set
            {
                showResultsList = value;
                Notify();
            }
            get => showResultsList;
        }

        private bool showMenuList;
        public bool ShowMenuList
        {
            set
            {
                showMenuList = value;
                Notify();
            }
            get => showMenuList;
        }

        private List<City> searchResults;
        public List<City> SearchResults
        {
            set
            {
                searchResults = value;
                Notify();
            }
            get => searchResults ?? (searchResults = new List<City>());
        }

        private bool searchBarIsEnabled;
        public bool SearchBarIsEnabled
        {
            set
            {
                searchBarIsEnabled = value;
                Notify();
            }
            get => searchBarIsEnabled;
        }

        public MainViewModel(IIOService<List<City>> service)
        {
            iOService = service;

            SearchBarIsEnabled = ShowMenuList = true;
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
            List<City> tmp_cities = new List<City>();
            foreach (var item in MenuItems)
                tmp_cities.Add(((WeatherViewModel)item.View.BindingContext).ViewModels[0].CurrentWeather.City);
            iOService.Save(tmp_cities);
        }

        public void LoadMenuItems()
        {
            List<City> users_cities = iOService.Load();
            if (users_cities == null || users_cities.Count == 0)
            {
                City city = new City(this) { Name = "Kyiv", Country = "UA", Id = 703448 };
                MenuItems.Add(new MasterPageItem() { Title = "Kiev", View = new WeatherView(city), TextColor = Color.White });
            }
            else
            {
                foreach (var city in users_cities)
                {
                    city.MainViewModel = this;
                    MenuItems.Add(new MasterPageItem()
                    {
                        Title = city.Name,
                        View = new WeatherView(city),
                        TextColor = Color.White
                    });
                }
            }

        }

    }
}
