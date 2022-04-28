using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TakDotNet.Application.Common;
using TakDotNet.Domain.Common;

// Setting up the configuartion for serilog 
ConfigurationBuilder builder = new();
BuildConfig(builder);

// Setting up the logger 
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Application Starting..");

// Setting up dependency injection
IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<ICustomService, EntryService>();
    })
    .UseSerilog()
    .Build();

var svc = ActivatorUtilities.CreateInstance<EntryService>(host.Services, args);
svc.Run(args);

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Productions"}.json", optional: true)
        .AddEnvironmentVariables();
}