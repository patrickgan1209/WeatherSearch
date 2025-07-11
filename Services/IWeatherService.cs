using WeatherSearch.Models;

namespace WeatherSearch.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather?> GetCurrentWeatherAsync(string name);
    }

}