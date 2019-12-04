using System;
using System.Threading.Tasks;
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
            MainPage = new LoadingPage();
            GetView();
        }

        private Task<MainView> InitView()
        {
            return Task.Factory.StartNew(() => new MainView());
        }

        private async void GetView()
        {
            MainPage = await InitView();
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
