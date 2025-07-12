namespace WeatherSearch.Models
{
    public class CurrentWeather
    {
        //City name
        public string Name { get; set; } = string.Empty;        
        //Country code
        public string Country { get; set; } = string.Empty;
        //Exact temperature 
        public double Temp { get; set; }
        //Humidity
        public int Humidity { get; set; }        
        // Wind speed
        public double Wind_Speed { get; set; }        
        //Wind direction
        public int Wind_Deg { get; set; }       
        //Weather condition within the group 
        public string Description { get; set; } = string.Empty;
         //Weather icon id
        public string Icon { get; set; } = string.Empty;
    }

}