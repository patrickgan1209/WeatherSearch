# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/WeatherSearch/WeatherSearch.csproj WeatherSearch/
RUN dotnet restore WeatherSearch/WeatherSearch.csproj

COPY src/WeatherSearch/ ./WeatherSearch/
WORKDIR /src/WeatherSearch
RUN dotnet publish WeatherSearch.csproj -c Release -o /app

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","WeatherSearch.dll"]
