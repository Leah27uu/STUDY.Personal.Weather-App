using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_App.Models
{
    public class WeatherResponse
    {
        [JsonProperty("name")]
        public string City {  get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("weather")]

        public WeatherInfo[] Weather { get; set; }



    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

    }

    public class WeatherInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
