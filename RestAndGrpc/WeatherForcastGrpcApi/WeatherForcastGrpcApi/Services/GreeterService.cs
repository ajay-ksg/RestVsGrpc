using Grpc.Core;

namespace WeatherForcastGrpcApi.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private static int _counter = 0;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
           Message = _counter + File.ReadAllText("./ResponseFile/Response.txt")
        });

    }
    
    public override Task<HelloReply> SayHello_100kb(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        var obj = new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response.txt")
        };
        
        Console.WriteLine("Object Size ##########----->>"+obj.CalculateSize());
        return Task.FromResult(obj);

    }
    public override Task<HelloReply> SayHello_500kb(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        var obj = new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_1.txt")
        };
        Console.WriteLine("Object Size ##########----->>"+obj.CalculateSize());
        return Task.FromResult(obj);

    }
    public override Task<HelloReply> SayHello_1MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_2.txt")
        });

    }
    public override Task<HelloReply> SayHello_5MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_3.txt")
        });

    }
    public override Task<HelloReply> SayHello_10MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_4.txt")
        });

    }
    public override Task<HelloReply> SayHello_15MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_5.txt")
        });

    }
    public override Task<HelloReply> SayHello_20MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_6.txt")
        });

    }
    public override Task<HelloReply> SayHello_25MB(HelloRequest request, ServerCallContext context)
    {
        _counter++;
        return Task.FromResult(new HelloReply()
        {
            Message = _counter + File.ReadAllText("./ResponseFile/Response_7.txt")
        });

    }
}