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
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    class MainViewModel : Notifier, IMainViewModel
    {
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
        private MainView mainView;
        public MainViewModel(MainView mainView)
        {
            this.mainView = mainView;
            City city = new City() { Name = "Kyiv", Country = "UA", Id = 703448 };
            MenuItems.Add(new MasterPageItem() { Title = "Kiev", View = new WeatherView(city), TextColor = Color.White });

            LoadCities();
        }

        public async void LoadCities()
        {
            NetworkManager networkManager = new NetworkManager();
            Cities = await networkManager.GetCities();

            City city = await FindCity(4158224);
            MenuItems.Add(new MasterPageItem() { Title = city.Name, View = new WeatherView(city), TextColor = Color.White });
            mainView.RefreshListView();
        }

        public Task<City> FindCity(int id)
        {
            return Task.Factory.StartNew(() => Cities.Where(x => x.Id == id).First());
        }
    }
}
