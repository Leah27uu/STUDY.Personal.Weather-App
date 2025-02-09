using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Weather_App.Models;

namespace Weather_App.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly String _apiKey = "c9911748e20197dbad0facee158b916f"; // Weather api key

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            try
            {
                // Debugging: Show the city being requested
                MessageBox.Show($"Fetching weather for: {city}");

                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Debugging: Show raw JSON response
                    MessageBox.Show($"API Response: {json}");

                    WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(json);
                    return weather;
                    
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch weather data." + ex.Message);
            }
        }
    }
}

