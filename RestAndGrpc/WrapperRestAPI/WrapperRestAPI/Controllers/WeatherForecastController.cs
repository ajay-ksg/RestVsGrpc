using System.Diagnostics;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using WeatherForcastGrpcApi;

namespace WrapperRestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private string grpcEndpoint = "http://3.144.157.205:1234"; // base url to grpc service
    private string restEndpoint = "http://3.144.157.205:8080"; // base url for rest service
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    #region restApi

    [HttpGet]
    [Route("/http1/compress/stringResponse/100KB")]
    public async Task<string> GetRestApiStringResponseV1()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();

        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint + "/stringRes/100KB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response = await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }

    [HttpGet]
    [Route("/http1/compress/stringResponse/500KB")]
    public async Task<string> GetRestApiStringResponseV7()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();


        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint + "/stringRes/500KB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response = await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }

    [HttpGet]
    [Route("/http1/compress/stringResponse/1MB")]
    public async Task<string> GetRestApiStringResponseV8()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();


        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint + "/stringRes/1MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response = await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }

    [HttpGet]
    [Route("/http1/compress/stringResponse/5MB")]
    public async Task<string> GetRestApiStringResponseV90()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();


        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint + "/stringRes/5MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response = await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }

    [HttpGet]
    [Route("/http1/compress/stringResponse/10MB")]
    public async Task<string> GetRestApiStringResponse10Mb()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();


        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint + "/stringRes/10MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response = await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }

    #endregion

    #region gRPC

    [HttpGet]
    [Route("/gRpcApi/100KB")]
    public async Task<string> GetGrpcApiWeatherForecastV3()
    {
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });

        using var channel = GrpcChannel.ForAddress(grpcEndpoint);
        //var client = new Greeter.GreeterClient(channel);
        var client = new Greeter.GreeterClient(channel);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var reply = client.SayHello_100kb(new HelloRequest());
        stopwatch.Stop();

        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return ("Greeting: " + reply);
    }

    [HttpGet]
    [Route("/gRpcApi/ServerCompression/500KB")]
    public async Task<string> GetGrpcApiWeatherForecastV5()
    {
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });


        using var channel = GrpcChannel.ForAddress(grpcEndpoint);
        //var client = new Greeter.GreeterClient(channel);
        var client = new Greeter.GreeterClient(channel);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var reply = client.SayHello_500kb(new HelloRequest());
        stopwatch.Stop();

        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return ("Greeting: " + reply);
    }

    [HttpGet]
    [Route("/gRpcApi/ServerCompression/1MB")]
    public async Task<string> GetGrpcApiWeatherForecastV1()
    {
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });


        using var channel = GrpcChannel.ForAddress(grpcEndpoint);
        //var client = new Greeter.GreeterClient(channel);
        var client = new Greeter.GreeterClient(channel);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var reply = client.SayHello_1MB(new HelloRequest());
        stopwatch.Stop();

        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return ("Greeting: " + reply);
    }

    [HttpGet]
    [Route("/gRpcApi/ServerCompression/5MB")]
    public async Task<string> GetGrpcApiWeatherForecastV9()
    {
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });


        using var channel = GrpcChannel.ForAddress(grpcEndpoint);
        //var client = new Greeter.GreeterClient(channel);
        var client = new Greeter.GreeterClient(channel);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var reply = client.SayHello_5MB(new HelloRequest());
        stopwatch.Stop();

        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return ("Greeting: " + reply);
    }

    [HttpGet]
    [Route("/gRpcApi/ServerCompression/10MB")]
    public async Task<string> GetGrpcApiWeatherForecast10Mb()
    {
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });


        using var channel = GrpcChannel.ForAddress(grpcEndpoint);
        //var client = new Greeter.GreeterClient(channel);
        var client = new Greeter.GreeterClient(channel);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var reply = client.SayHello_10MB(new HelloRequest());
        stopwatch.Stop();

        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return ("Greeting: " + reply);
    }

    #endregion
}