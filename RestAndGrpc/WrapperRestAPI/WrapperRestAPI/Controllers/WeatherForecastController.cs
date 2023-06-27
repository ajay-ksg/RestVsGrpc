using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Headers;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.Compression;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherForcastGrpcApi;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace WrapperRestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private string grpcEndpoint = "http://3.144.157.205:1234";
    private string restEndpoint = "http://3.144.157.205:8080";
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
#region restApi
    [HttpGet]
    [Route("/resApi/")]
    public IEnumerable<WeatherForecast> GetRestApiWeatherForecast()
    {
        HttpClient client = new HttpClient();
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        string response = "";
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:9000/getWeatherInfo/"))
        {
            requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            stopwatch.Start();
            var result = client.Send(requestMessage);
            response =  result.Content.ReadAsStringAsync().Result;
            stopwatch.Stop();
        }
        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        
        var ret = JsonConvert.DeserializeObject<List<WeatherForecast>>(response);
        
        return ret;

    }
    [HttpGet]
    [Route("/http2/compress/stringResponse/")]
    public string GetRestApiStringResponseV2()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();

        string response = "";
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/"))
        {
            requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            stopwatch.Start();
            var result = client.Send(requestMessage);
            response =  result.Content.ReadAsStringAsync().Result;
            stopwatch.Stop();
        }
        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return response;

    }
    
    [HttpGet]
    [Route("/http1/compress/stringResponse/100KB")]
    public async Task<string> GetRestApiStringResponseV1()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();


        var response = "";

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/100KB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response =  await res.Content.ReadAsStringAsync();
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

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/500KB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response =  await res.Content.ReadAsStringAsync();
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

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/1MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response =  await res.Content.ReadAsStringAsync();
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

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/5MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response =  await res.Content.ReadAsStringAsync();
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

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, restEndpoint+"/stringRes/10MB"))
        {
            stopwatch.Start();
            var res = client.Send(requestMessage);
            response =  await res.Content.ReadAsStringAsync();
            stopwatch.Stop();
        }

        return response;
    }
    

    [HttpGet]
    [Route("/http2/stringResponse/")]
    public string GetRestApiStringResponse_noCompressV2()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();

        string response = "";
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8084/stringRes/"))
        {
            stopwatch.Start();
            var result = client.Send(requestMessage);
            response =  result.Content.ReadAsStringAsync().Result;
            stopwatch.Stop();
        }
        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return response;

    }
    
    [HttpGet]
    [Route("/http1/stringResponse/")]
    public string GetRestApiStringResponse_noCompressV1()
    {
        HttpClient client = new HttpClient();

        var stopwatch = new Stopwatch();

        string response = "";
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8086/stringRes/"))
        {
            stopwatch.Start();
            var result = client.Send(requestMessage);
            response =  result.Content.ReadAsStringAsync().Result;
            stopwatch.Stop();
        }
        Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
        return response;

    }

#endregion

#region gRPC

    // [HttpGet]
    // [Route("/gRpcApi/ClientCompression/headers/")]
    // public async Task<string> GetGrpcApiWeatherForecastV2()
    // {
    //     var loggerFactory = LoggerFactory.Create(logging =>
    //     {
    //         logging.AddConsole();
    //         logging.SetMinimumLevel(LogLevel.Debug);
    //
    //     });
    //    
    //     var channelOptions = new GrpcChannelOptions()
    //     {
    //         MaxReceiveMessageSize = 12*1024*1024,
    //         LoggerFactory = loggerFactory
    //     };
    //     using var channel = GrpcChannel.ForAddress("http://localhost:8080",channelOptions);
    //     var client = new Greeter.GreeterClient(channel);
    //     var headers = new Metadata();
    //     headers.Add("grpc-internal-encoding-request", "gzip");
    //     headers.Add("grpc-accept-encoding", "gzip");
    //     headers.Add("requester", "Wrapper_client");
    //     var callOptions = new CallOptions().WithHeaders(headers).WithDeadline(DateTime.UtcNow.AddSeconds(15)).WithWaitForReady(false);
    //     
    //     var stopwatch = new Stopwatch();
    //     stopwatch.Start();
    //     var reply =  await client.SayHelloAsync(
    //         new HelloRequest() { Name = "GreeterClient" },callOptions);
    //     
    //     stopwatch.Stop();
    //     Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
    //     return ("Greeting: " + reply);
    // }
    //
    // [HttpGet]
    // [Route("/gRpcApi/ClientCompression/channelOption/")]
    // public async Task<string> GetGrpcApiWeatherForecast()
    // {
    //     var loggerFactory = LoggerFactory.Create(logging =>
    //     {
    //         logging.AddConsole();
    //         logging.SetMinimumLevel(LogLevel.Debug);
    //
    //     });
    //    
    //     var channelOptions = new GrpcChannelOptions()
    //     {
    //         MaxReceiveMessageSize = 12*1024*1024,
    //         CompressionProviders = new Collection<ICompressionProvider>
    //         {
    //             new GzipCompressionProvider(CompressionLevel.Fastest)
    //         },
    //         LoggerFactory = loggerFactory
    //         
    //     };
    //     using var channel = GrpcChannel.ForAddress("http://localhost:8080",channelOptions);
    //     var client = new Greeter.GreeterClient(channel);
    //     var stopwatch = new Stopwatch();
    //     stopwatch.Start();
    //     var reply =  await client.SayHelloAsync(
    //         new HelloRequest() { Name = "GreeterClient" });
    //     
    //     stopwatch.Stop();
    //     
    //     Console.WriteLine("Time Consumed for getting data is :::: " + stopwatch.ElapsedMilliseconds);
    //     return ("Greeting: " + reply.Message);
    // }
    //
    [HttpGet]
    [Route("/gRpcApi/ServerCompression/")]
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

