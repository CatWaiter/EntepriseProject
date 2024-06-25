using System.Text.Json;
using EnterpriseProject.Models;

namespace EnterpriseProject.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
    }

    public async Task<WeatherData> GetCurrentWeatherAsync(string city)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=imperial";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"API call failed with status code {response.StatusCode}: {content}");
            return null;
        }

        try
        {
            var weatherData = JsonSerializer.Deserialize<WeatherData>(content);
            return weatherData;
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Deserialization error: {e.Message} for content: {content}");
            return null;
        }
    }
}