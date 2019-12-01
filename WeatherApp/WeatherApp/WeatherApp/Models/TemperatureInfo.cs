using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class TemperatureInfo
    {
        public float Temp { set; get; }
        public float Temp_min { set; get; }
        public float Temp_max { set; get; }

        [JsonIgnore]
        public int TempInt => Convert.ToInt32(Temp);
        [JsonIgnore]
        public int Temp_minInt => Convert.ToInt32(Temp_min);
        [JsonIgnore]
        public int Temp_maxInt => Convert.ToInt32(Temp_max);
    }
}
