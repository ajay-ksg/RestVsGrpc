using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace WeatherForcastRestApi.Controllers;
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static int _counter = 0;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/getWeatherInfo/")]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return Enumerable.Range(1, 50000).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet]
    [Route("/stringRes/100KB")]
    public string GetStringResponse_100KB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response.txt");
        Console.WriteLine("Object Size ##########----->>"+response.Length);
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/500KB")]
    public string GetStringResponse_500KB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_1.txt");
        Console.WriteLine("Object Size ##########----->>"+response.Length);
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/1MB")]
    public string GetStringResponse_1MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_2.txt");
        Console.WriteLine("Object Size ##########----->>"+response.Length);
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/5MB")]
    public string GetStringResponse_5MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_3.txt");
        Console.WriteLine("Object Size ##########----->>"+response.Length);
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/10MB")]
    public string GetStringResponse_10MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_4.txt");
        Console.WriteLine("Object Size ##########----->>"+response.Length);
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/15MB")]
    public string GetStringResponse_15MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_5.txt");
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/20MB")]
    public string GetStringResponse_20MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_6.txt");
        return _counter + response;
    }
    [HttpGet]
    [Route("/stringRes/25MB")]
    public string GetStringResponse_25MB()
    {
        _counter++;
        var response = System.IO.File.ReadAllText("./ResponseFile/Response_7.txt");
        return _counter + response;
    }
}