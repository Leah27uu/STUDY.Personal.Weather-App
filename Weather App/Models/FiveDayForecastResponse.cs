using Newtonsoft.Json;
using System;

namespace Weather_App.Models
{
    public class FiveDayForecastResponse
    {
        [JsonProperty("list")]
        public ForecastItem[] Forecasts { get; set; }
    }

    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long DateUnix { get; set; }

        [JsonProperty("main")]
        public Temperature Main { get; set; }

        [JsonProperty("weather")]
        public WeatherInfo[] Weather { get; set; }

        public string FormattedDate => DateTimeOffset.FromUnixTimeSeconds(DateUnix).DateTime.ToShortDateString();
    }

    public class Temperature
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}