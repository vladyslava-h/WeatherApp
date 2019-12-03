using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Infrastructure;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.IoC
{
    class ServiceLocator
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Setup()
        {
            Kernel.Bind<IIOService<List<City>>>().To<JsonService>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public MainViewModel MainViewModel
        {
            get => Kernel.Get<MainViewModel>();
        }

    }
}
