using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using WeatherApp.IoC;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        IMainViewModel mainViewModel;
        public MainView()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(ServiceLocator.Get<IIOService<List<City>>>());
            this.BindingContext = mainViewModel;
            Detail = new NavigationPage(mainViewModel.MenuItems[0].View);
            listView.SelectedItem = mainViewModel.MenuItems[0];
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            await Task.Run(() =>
            {
                var item = (MasterPageItem)e.SelectedItem;
                mainViewModel.SelectedItem = item;
                WeatherView page = item.View;

                Task.Delay(300).Wait();
                Device.BeginInvokeOnMainThread(() =>
                {
                    Detail = new NavigationPage(page);
                });
            });
        }

    }
}