// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherSearch.Data;
using WeatherSearch.Models;
using WeatherSearch.Services;

namespace WeatherSearch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWeatherService _weatherService;
        private readonly WeatherDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IWeatherService weatherService, WeatherDbContext context, ILogger<IndexModel> logger)
        {
            _weatherService = weatherService;
            _context = context;
            _logger = logger;
        }

        public CurrentWeather? Weather { get; set; }
        public List<SearchedLocations> SearchedLocations { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public string? SearchCity { get; set; }

        public async Task OnGetAsync()
        {
            await LoadSearchedLocations();
        }

        public async Task<IActionResult> OnPostSearchNameAsync(string name)
        {
            SearchCity = name;

            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage = "Please enter a city name.";
                await LoadSearchedLocations();
                return Page();
            }

            Weather = await _weatherService.GetCurrentWeatherAsync(name);

            if (Weather == null)
            {
                ErrorMessage = $"Could not find weather data for '{name}'. Please check the city name and try again.";
            }
            else
            {
                // Save the location
                await SaveLocationAsync(Weather.Name, Weather.Country);

            }

            await LoadSearchedLocations();
            return Page();
        }

        public async Task<IActionResult> OnPostLoadLocationAsync(string name, string country)
        {
            SearchCity = name;
            Weather = await _weatherService.GetCurrentWeatherAsync(name);

            if (Weather == null)
            {
                ErrorMessage = $"Could not load current weather for {name}.";
            }
            await SaveLocationAsync(name, country);

            await LoadSearchedLocations();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var location = await _context.SearchedLocations.FindAsync(id);
            if (location != null)
            {
                _context.SearchedLocations.Remove(location);
                await _context.SaveChangesAsync();
            }

            await LoadSearchedLocations();
            return Page();
        }

        private async Task SaveLocationAsync(string name, string country)
        {
            var existing = await _context.SearchedLocations
                .FirstOrDefaultAsync(l => l.Name == name && l.Country == country);

            if (existing != null)
            {
                _context.SearchedLocations.Remove(existing);
            }
            _context.SearchedLocations.Add(new SearchedLocations
            {
                Name = name,
                Country = country,
                SearchedDatetime = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }

        private async Task LoadSearchedLocations()
        {
            SearchedLocations = await _context.SearchedLocations
                .OrderByDescending(l => l.SearchedDatetime)
                .ToListAsync();
        }
    }
}