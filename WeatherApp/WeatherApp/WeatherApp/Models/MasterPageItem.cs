using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Infrastructure;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    public class MasterPageItem : Notifier
    {
        public string Title { set; get; }
        public WeatherView View { set; get; }
        private Color textColor;
        public Color TextColor 
        {
            set
            {
                textColor = value;
                Notify();
            }
            get => textColor;
        }
    }
}
