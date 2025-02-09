using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weather_App.Models;
using Weather_App.Services; // Import WeatherService

namespace Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public readonly WeatherService _weatherService;
        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherService();

        }

        // Event: Hide Placeholder when typing
        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PlaceholderText != null)
            {
                PlaceholderText.Visibility = string.IsNullOrWhiteSpace(CityTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        // Event: Fetch Weather When Button is Clicked
        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            // Debugging: Check what text is being retreived from the input box
            MessageBox.Show($"Tect Entered: {city}");

            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Please enter a city name.");
                return;
            }

            try
            {
                WeatherResponse weather = await _weatherService.GetWeatherAsync(city);
                WeatherTextBlock.Text = $"City: {weather.City}\nTemperature: {weather.Main.Temperature}°C\nHumidity: {weather.Main.Humidity}%\nCondition: {weather.Weather[0].Description}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching weather data: " + ex.Message);
            }
        }
    }
}