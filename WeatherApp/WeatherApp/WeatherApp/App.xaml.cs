using System;
using WeatherApp.Infrastructure;
using WeatherApp.IoC;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceLocator.Setup();
            MainPage = new MainView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            ((IMainViewModel)MainPage.BindingContext).SaveCities();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
