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
        [JsonProperty("timezone")]
        public string ?Timezone { get; set; }

        [JsonProperty("daily")]
        public DailyWeather[] ?Daily { get; set; }
    }

    public class DailyWeather
    {
        [JsonProperty("dt")]
        public long DateUnix { get; set; }

        [JsonProperty("temp")]
        public Temperature Temp { get; set; }

        [JsonProperty("weather")]
        public WeatherInfo[] Weather { get; set; }

        public string FormattedDate => DateTimeOffset.FromUnixTimeSeconds(DateUnix).DateTime.ToShortDateString();
    }


    //public class Temperature
    //{
    //    [JsonProperty("day")]
    //    public double Day { get; set; }
    //}

    //public class WeatherInfo
    //{
    //    [JsonProperty("description")]
    //    public string ?Description { get; set; }

    //    [JsonProperty("icon")]
    //    public string ?Icon { get; set; } 
    //}
}
