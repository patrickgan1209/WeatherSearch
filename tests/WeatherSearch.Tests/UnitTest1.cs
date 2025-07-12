using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherSearch.Services;
using Xunit;

namespace WeatherSearch.Tests;

public class WeatherServiceTests
{
    // --------------- helpers ---------------
    private static WeatherService CreateSut()
    {
        // fake HTTP (not used in GetGreetingForTemp but required by ctor)
        var httpClient = new HttpClient(new FakeHttpMessageHandler());

        // dummy config / logger
        var config = new ConfigurationBuilder().Build();
        var logger = LoggerFactory.Create(b => b.AddDebug()).CreateLogger<WeatherService>();

        return new WeatherService(httpClient, config, logger);
    }

    // --------------- tests ---------------
    [Fact]
    public void AlwaysPasses() => Assert.True(true);

    [Fact]
    public void WhenTempAbove25()
    {
        var service = CreateSut();
        var result  = service.GetGreetingForTemp(30);
        Console.WriteLine($"Returned string = \"{result}\"");

        Assert.Equal("It's hot!", result);
    }

    [Fact]
    public void WhenTemp25OrLess()
    {
        var service = CreateSut();
        var result  = service.GetGreetingForTemp(22);
        
        Console.WriteLine($"Returned string = \"{result}\"");

        Assert.Equal("Nice weather!", result);
    }
}

// --------------- fake HTTP handler ---------------
public class FakeHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
           {
               Content = new StringContent("{\"weather\":[],\"main\":{\"temp\":28.0}}")
           });
}
