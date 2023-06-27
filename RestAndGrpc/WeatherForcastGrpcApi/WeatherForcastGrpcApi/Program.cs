using System.IO.Compression;
using Grpc.Core;
using Grpc.Core.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WeatherForcastGrpcApi.Services;
using CompressionLevel = System.IO.Compression.CompressionLevel;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// builder.WebHost.ConfigureKestrel(options =>
// {
//     // Setup a HTTP/2 endpoint without TLS.
//     options.ListenLocalhost(1234, o => o.Protocols =
//         HttpProtocols.Http2);
// });
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
var b = builder.Services.AddGrpc(o =>
{
    o.ResponseCompressionLevel = CompressionLevel.Optimal;
    o.ResponseCompressionAlgorithm = "gzip";
    o.MaxSendMessageSize = 30 * 1024 * 1024;
    o.MaxReceiveMessageSize = 30 * 1024 * 1024;
});

b.Services.AddGrpc();
Environment.SetEnvironmentVariable("GRPC_TRACE", "all");
Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "DEBUG");
GrpcEnvironment.SetLogger(new ConsoleLogger());

b.Services.AddResponseCompression();

var app = builder.Build();

app.UseResponseCompression();
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();