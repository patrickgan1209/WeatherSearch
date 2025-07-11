using System.Text.Json;
using WeatherSearch.Models;

namespace WeatherSearch.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<CurrentWeather?> GetCurrentWeatherAsync(string name)
        {
            try
            {
                var apiKey = _configuration["OpenWeatherMapApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogError("OpenWeatherMap API key is not configured");
                    return null;
                }

                var url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(name)}&appid={apiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Weather API returned {StatusCode} for city: {City}", response.StatusCode, name);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(json);
                var root = document.RootElement;

                return new CurrentWeather
                {
                    Name = root.GetProperty("name").GetString() ?? name,                    
                    Country = root.GetProperty("sys").GetProperty("country").GetString() ?? "",
                    Temp = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                    Wind_Speed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                    Wind_Deg = root.GetProperty("wind").GetProperty("deg").GetInt32(),
                    Icon  = root.GetProperty("weather")[0].GetProperty("icon").GetString() ?? "",
                    Description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                    
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather for city: {City}", name);
                return null;
            }
        }
    }
}