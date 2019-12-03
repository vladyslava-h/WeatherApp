using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Infrastructure
{
    interface IIOService<T>
    {
        void Save(T data);
        T Load();
    }
}
