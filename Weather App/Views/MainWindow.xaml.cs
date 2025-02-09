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
using System.Windows.Media.Animation; // Required for animations
using System.Media;
using Weather_App.Models; // Required for playing system sounds


namespace Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly WeatherService _weatherService;
        private bool isPlaceholderHidden = false; // Track placeholder state
        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherService();

        }

        // Event: Hide Placeholder when typing
        private void CityTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PlaceholderText != null)
            {
                if (string.IsNullOrWhiteSpace(CityTextBox.Text))
                {
                    // Play Fade In only when placeholder is hidden
                    if (isPlaceholderHidden)
                    {
                        PlaceholderText.Visibility = Visibility.Visible;
                        Storyboard fadeIn = (Storyboard)CityTextBox.FindResource("PlaceholderFadeIn");
                        fadeIn.Begin(PlaceholderText);
                        isPlaceholderHidden = false; // Track state
                    }
                }
                else
                {
                    // Play Fade Out only once when placeholder is visible
                    if (!isPlaceholderHidden)
                    {
                        Storyboard fadeOut = (Storyboard)CityTextBox.FindResource("PlaceholderFadeOut");
                        fadeOut.Begin(PlaceholderText);
                        isPlaceholderHidden = true; // Track state
                    }
                }
            }
        }

        // Event: Fetch Weather When Button is Clicked
        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(city))
            {
                WeatherTextBlock.Text = "Please enter a city name.";
                SystemSounds.Exclamation.Play(); // Plays error sound
                //MessageBox.Show("Please enter a city name.");
                return;
            }

            WeatherTextBlock.Text = "Fetching weather..."; // Show loading message
            WeatherIcon.Visibility = Visibility.Collapsed; // Hide icon while loading

            try
            {
                FiveDayForecastResponse forecast = await _weatherService.GetFiveDayForecastAsync(city);
                //WeatherResponse? weather = await _weatherService.GetWeatherAsync(city);

                if (forecast == null || forecast.Forecasts == null || forecast.Forecasts.Length == 0)
                {
                    throw new Exception("Failed to retrieve weather data.");
                }

               
                ForecastListBox.ItemsSource = forecast.Forecasts.Select(day => new
                {
                    FormattedDate = day.FormattedDate,
                    Temperature = $"{day.Main.Temp}°C",
                    Description = day.Weather[0].Description,
                    IconUrl = $"https://openweathermap.org/img/wn/{day.Weather[0].Icon}@2x.png"
                }).ToList();




                string iconCode = forecast.Forecasts[0].Weather[0].Icon ?? "01d";
                string iconUrl = $"https://openweathermap.org/img/wn/{iconCode}@2x.png";

     
                WeatherIcon.Source = new BitmapImage(new Uri(iconUrl, UriKind.Absolute));
                

                // Download and display the weather icon
                WeatherIcon.Source = new BitmapImage(new Uri(iconUrl));
                WeatherIcon.Visibility = Visibility.Visible; // Show icon when loaded

                // Updates the weather text
                WeatherTextBlock.Text = $"City: {city ?? "Unknown"}\n" +
                                        $"Temperature: {forecast.Forecasts[0].Main.Temp}°C\n" +
                                        $"Condition: {forecast.Forecasts[0].Weather[0].Description ?? "Unknown"}";
            }
            catch (Exception ex)
            {
                
                WeatherTextBlock.Text = ex.Message;
                SystemSounds.Hand.Play(); // Plays error sound
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}