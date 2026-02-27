using Common.Utilities;
using CoreDBIterator.Workers;
using CoreDBModel.Extensions;
using Pushables;
using Serilog;
using SolutionConfiguration;

// For a non-web Worker Service use the generic Host builder and register Serilog on the host
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).AddEnvironmentVariables().Build())
    .CreateLogger();

var solutionEnvironment = await new ConfigurationClient().GetConfigurationAsync();

var hostBuilder = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
                     .ReadFrom.Services(services)
                     .Enrich.FromLogContext())
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<ConverterToDateTimeService>();
        services.AddHttpClient<PublisherService>()
                .AddTypedClient((httpClient, sp) => new PublisherService(solutionEnvironment.PublisherService.PublicUrl, httpClient));

        services.AddHostedService<DatabaseMigratorExecutor>();
        services.AddHostedService<LessonCreatingExecutor>();
        services.AddHostedService<LessonWarningExecutor>();
        services.AddHostedService<LessonIteratorExecutor>();

        services.AddCoreContext(solutionEnvironment.CoreDatabaseConnection);
    });

var host = hostBuilder.Build();
host.Run();
