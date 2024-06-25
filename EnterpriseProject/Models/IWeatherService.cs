using EnterpriseProject.Services;

namespace EnterpriseProject.Models;

public interface IWeatherService
{
    Task<WeatherData> GetCurrentWeatherAsync(string city);
}