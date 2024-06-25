using System.Text.Json.Serialization;

namespace EnterpriseProject.Services;

public class WeatherData
{
    public class TemperatureInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        [JsonPropertyName("main")]
        public string Main { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    [JsonPropertyName("main")]
    public TemperatureInfo Main { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("weather")]
    public IEnumerable<WeatherDescription> Weather { get; set; }
}

