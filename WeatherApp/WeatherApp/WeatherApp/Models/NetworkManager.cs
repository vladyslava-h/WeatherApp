using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class NetworkManager
    {
        public CurrentWeather GetCurrentWeather(int id)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?id={id}&units=metric&appid=921e83b9da8a40a760ad74d5cedd6bbd";

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


        public Forecast GetForecast(int id)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?id={id}&units=metric&appid=921e83b9da8a40a760ad74d5cedd6bbd";

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

        public Task<List<City>> GetCities()
        {
            return Task<List<City>>.Factory.StartNew(() =>
            {
                string url = $"https://raw.githubusercontent.com/vladislava-g/OpenWeatherMapCityList/master/city.list.min.json";
                WebClient client = new WebClient();
                string responseJson = string.Empty;
                Stream stream = client.OpenRead(url);
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    responseJson = streamReader.ReadToEnd();
                }
                stream.Close();
                
                return JsonConvert.DeserializeObject<List<City>>(responseJson);
            });

        }

    }
}
