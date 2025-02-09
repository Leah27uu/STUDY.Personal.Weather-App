using Newtonsoft.Json;

namespace Weather_App.Models 
{
    public class CityWeatherResponse
    {
        [JsonProperty("name")]
        public string City { get; set; }

        [JsonProperty("coord")]
        public Coordinates Coord { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
