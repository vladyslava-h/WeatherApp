using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.Infrastructure
{
    public interface IMainViewModel
    {
        ObservableCollection<MasterPageItem> MenuItems { set; get; }
        MasterPageItem SelectedItem { set; get; }
        City SelectedCityToRemove { set; get; }
        void SaveCities();
    }
}
