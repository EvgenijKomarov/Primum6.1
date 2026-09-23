using CoreDBIterator.Extensions;
using Serilog;

// For a non-web Worker Service use the generic Host builder and register Serilog on the host
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).AddEnvironmentVariables().Build())
    .CreateLogger();

var hostBuilder = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
                     .ReadFrom.Services(services)
                     .Enrich.FromLogContext())
    .ConfigureServices((context, services) => services.AddCoreDbIteratorServices());

var host = hostBuilder.Build();
host.Run();
