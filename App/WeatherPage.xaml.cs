using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace EnterpriseMarketplace
{
    public partial class WeatherPage : ContentPage
    {
        private const string ApiKey = "59e68f7257f04d5dc6cf3ef4712a97f8";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public string Temperature { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public WeatherPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadWeatherDataAsync();
        }

        private async void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            await LoadWeatherDataAsync();
        }

        private async Task LoadWeatherDataAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{BaseUrl}?q=Pittsburgh&appid={ApiKey}&units=imperial";
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                        Temperature = $"Temperature: {weatherData.Main.Temp}°F";
                        Description = $"Conditions: {weatherData.Weather[0].Description}";
                        Location = $"Location: {weatherData.Name}";
                        OnPropertyChanged(nameof(Temperature));
                        OnPropertyChanged(nameof(Description));
                        OnPropertyChanged(nameof(Location));
                    }
                    else
                    {
                        Temperature = "Failed to load weather data: " + response.StatusCode;
                        OnPropertyChanged(nameof(Temperature));
                    }
                }
            }
            catch (Exception ex)
            {
                Temperature = "Failed to load weather data: " + ex.Message;
                OnPropertyChanged(nameof(Temperature));
            }
        }
    }

    public class WeatherData
    {
        public WeatherInfo[] Weather { get; set; }
        public MainInfo Main { get; set; }
        public string Name { get; set; }

        public class WeatherInfo
        {
            public string Description { get; set; }
        }

        public class MainInfo
        {
            public float Temp { get; set; }
        }
    }
}