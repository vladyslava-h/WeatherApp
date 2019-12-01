using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class WeatherView : ContentPage
    {
        public WeatherView(City city)
        {
            InitializeComponent();
            this.BindingContext = new WeatherViewModel(city);
        }
    }
}
