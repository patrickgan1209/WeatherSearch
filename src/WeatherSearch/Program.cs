using Microsoft.EntityFrameworkCore;
using WeatherSearch.Data;

using WeatherSearch.Services;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

//Create Log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/weatherapp-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    // Use Serilog
    builder.Host.UseSerilog();

    // ... rest of your configuration



    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddDbContext<WeatherDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddHttpClient<IWeatherService, WeatherService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
        db.Database.EnsureCreated();
    }


    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapRazorPages();

    // Ensure database is created
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
        dbContext.Database.Migrate();
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}