using Common.Utilities;
using CoreDBIterator.Workers;
using CoreDBModel.Extensions;
using Pushables;
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
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<ConverterToDateTimeService>();

        string coreUrl = context.Configuration["PublisherURL"] ?? "https://localhost:5004";
        services.AddHttpClient<PublisherService>()
                .AddTypedClient((httpClient, sp) => new PublisherService(coreUrl, httpClient));

        services.AddHostedService<LessonCreatingExecutor>();
        services.AddHostedService<LessonWarningExecutor>();
        services.AddHostedService<LessonIteratorExecutor>();

        services.AddCoreContext(context.Configuration.GetConnectionString("DefaultConnection"));
    });

var host = hostBuilder.Build();
host.Run();
