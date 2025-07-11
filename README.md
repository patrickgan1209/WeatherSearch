# Weather Search (Razor Pages)

A simple ASP .NET Core Razor Pages app that lets you  

1. search current weather by city name (via OpenWeatherMap)  
2. save searches for one-click recall  
3. delete saved locations  

---

## Prerequisites

* .NET 8 SDK  
* Free **OpenWeatherMap** API key  

---

## Quick start

```bash
# 1 clone & restore
git clone https://github.com/your-name/WeatherSearch.git
cd WeatherSearch
dotnet restore

# 2 add your API key
dotnet user-secrets set "OpenWeatherMapApiKey" "<YOUR_KEY>" --project WeatherSearch.csproj

# 3 run the app
dotnet run --project WeatherSearch.csproj

# browse to:
# http://localhost:5227  (HTTPS on 7177 if preferred)
