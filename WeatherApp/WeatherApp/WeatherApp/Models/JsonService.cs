using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure;
using Xamarin.Essentials;

namespace WeatherApp.Models
{
    class JsonService : IIOService<List<City>>
    {
        public List<City> Load()
        {
            string filepath = Path.Combine(FileSystem.CacheDirectory, "cities.json");
            string responseJson = string.Empty;
            List<City> users_cities = new List<City>();

            try
            {
                if (File.Exists(filepath))
                {
                    using (StreamReader file = File.OpenText(filepath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        users_cities = (List<City>)serializer.Deserialize(file, typeof(List<City>));
                    }
                }
            }
            catch { }

            return users_cities;
        }

        public void Save(List<City> cities)
        {
            Task.Run(() =>
            {
                string filepath = Path.Combine(FileSystem.CacheDirectory, "cities.json");

                if (File.Exists(filepath))
                    File.Delete(filepath);

                using (StreamWriter sw = File.CreateText(filepath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    string json = JsonConvert.SerializeObject(cities);
                    sw.Write(json);
                }
                if (File.Exists(filepath))
                    Console.WriteLine();
            });
        }
    }
}
