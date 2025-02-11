using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Weather_App.Models;
using System.Media;
using static System.Net.WebRequestMethods;



namespace Weather_App.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly String _apiKey; // Weather api key
        
        //commit test

        public WeatherService()
        {
            _httpClient = new HttpClient();
            _apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY") ?? throw new Exception("API key not set in enviorment variables");
        }

        public async Task<CityWeatherResponse> GetCityCoordinatesAsync(string city)
        {
            try
            {
       
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

                //  Debug: Print the full API request
                Console.WriteLine("API Request: " + url); // Log the full request URL
                MessageBox.Show("API Request: " + url);   // Show in a message box for testing

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();             
                    CityWeatherResponse cityWeather = JsonConvert.DeserializeObject<CityWeatherResponse>(json);
                    
                    

                    if (cityWeather == null)
                    {
                        SystemSounds.Hand.Play();
                        throw new Exception("The response was empty or invalid.");
                    }
                    return cityWeather;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    SystemSounds.Hand.Play();
                    throw new Exception("City not found. Please enter a valid city.");
                }
               else
                {
                    SystemSounds.Hand.Play();
                    throw new Exception($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException)
            {
                SystemSounds.Hand.Play();
                throw new Exception("Network error. Please check your internet connection.");
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                throw new Exception(ex.Message);
            }
        }

        public async Task<FiveDayForecastResponse> GetFiveDayForecastAsync(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    FiveDayForecastResponse forecast = JsonConvert.DeserializeObject<FiveDayForecastResponse>(json);
                    return forecast;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("City not found. Please enter a valid city.");
                }
                else
                {
                    throw new Exception($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch weather forecast: " + ex.Message);
            }
        }
    }
}

