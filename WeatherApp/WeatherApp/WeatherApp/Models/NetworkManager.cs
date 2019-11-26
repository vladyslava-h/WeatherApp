using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            response.Close();
            return JsonConvert.DeserializeObject<CurrentWeather>(responseJson);
        }


        public Forecast GetForecast(string cityName)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?q={cityName}&units=metric&appid=921e83b9da8a40a760ad74d5cedd6bbd";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseJson = string.Empty;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseJson = streamReader.ReadToEnd();
            }

            Forecast list = JsonConvert.DeserializeObject<Forecast>(responseJson);

            response.Close();
            return list;
        }
    }
}
