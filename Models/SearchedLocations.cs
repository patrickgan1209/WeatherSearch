namespace WeatherSearch.Models
{
    public class SearchedLocations
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime SearchedDatetime { get; set; }
    }
}