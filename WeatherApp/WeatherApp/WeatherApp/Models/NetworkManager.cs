using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherApp.Models
{
    class NetworkManager
    {
        public CurrentWeather GetCurrentWeather(string cityName)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=921e83b9da8a40a760ad74d5cedd6bbd";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseJson = string.Empty;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseJson = streamReader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<CurrentWeather>(responseJson);
        }
    }
}
