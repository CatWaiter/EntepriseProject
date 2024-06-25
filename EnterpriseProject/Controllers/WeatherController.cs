using EnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Weather");
        }

        [HttpGet("Weather")]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                ViewBag.ErrorMessage = "Please enter a city name.";
                return View("Weather");
            }
            
            var weatherData = await _weatherService.GetCurrentWeatherAsync(city);
            if (weatherData == null)
            {
                ViewBag.ErrorMessage = "Weather data is not available. Try searching for another city.";
                return View("Weather");
            }
            return View("Weather", weatherData);
        }
    }
}