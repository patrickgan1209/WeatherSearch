# Weather Search (Razor Pages)


## Live Demo

You can access the deployed application here:  
https://ncys7mmdvs.ap-southeast-2.awsapprunner.com

---

A simple ASP .NET Core Razor Pages app that lets you  

1. search current weather by city name (via OpenWeatherMap)  
2. save searches for one-click recall  
3. delete saved locations  

---

## Prerequisites

* .NET 8 SDK  
* Free **OpenWeatherMap** API key  (Ref: https://openweathermap.org/api)
  Please contact me if you'd like a temporary API key for testing purposes.
  
---

## Quick start

```bash
# 1 clone & restore
git clone https://github.com/patrickgan1209/WeatherSearch.git
cd WeatherSearch
dotnet restore

# 2 add your API key
dotnet user-secrets set "OpenWeatherMapApiKey" "<YOUR_KEY>" --project src/WeatherSearch/WeatherSearch.csproj

# 3 run the app
dotnet run --project src/WeatherSearch/WeatherSearch/WeatherSearch.csproj

# browse to:
# http://localhost:5227  (HTTPS on 7177 if preferred)
