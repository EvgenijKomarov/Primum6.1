using ChatSigns;
using CoreConnection;
using Microsoft.EntityFrameworkCore;
using PrimumCore.BackgroundWorkers;
using PrimumCore.BackgroundWorkers.Executors;
using PrimumCore.Controllers;
using PrimumCore.Models;
using PrimumCore.Options;
using PrimumCore.Services.Iterators;
using PrimumCore.Services.Utilities;
using Pushables;
using Serilog;

namespace PrimumCore.Extentions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentIterator>();
            builder.Services.AddScoped<TeacherIterator>();
            builder.Services.AddScoped<AdminIterator>();
            builder.Services.AddScoped<UserIterator>();
            builder.Services.AddScoped<IncidentIterator>();
            builder.Services.AddScoped<CourseIterator>();
            builder.Services.AddScoped<SheduleIterator>();
            builder.Services.AddScoped<LessonIterator>();
            builder.Services.AddScoped<ThemeIterator>();
            builder.Services.AddScoped<GradingIterator>();
            builder.Services.AddScoped<PromocodeIterator>();
            builder.Services.AddScoped<AbonementIterator>();
            builder.Services.AddScoped<PasswordHasher>();
            builder.Services.AddScoped<ConverterToDateTimeService>();
            builder.Services.AddScoped<RandomStringGenerator>();
            builder.Services.AddScoped<TokenIterator>();
            builder.Services.AddScoped<AnonymousTokenIterator>();
            builder.Services.AddScoped<ChatSignTokenWorker>();

            return builder;
        }

        public static WebApplicationBuilder AddProjectControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentController>();
            builder.Services.AddScoped<AvailableUserController>();
            builder.Services.AddScoped<TeacherController>();
            builder.Services.AddScoped<AdminController>();
            builder.Services.AddScoped<PublicController>();

            return builder;
        }

        public static WebApplicationBuilder AddPeriodWorkers(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<LessonWarningWorker>();
            builder.Services.AddHostedService<LessonIteratorWorker>();
            builder.Services.AddHostedService<LessonCreatingWorker>();

            //executors
            builder.Services.AddSingleton<LessonWarningExecutor>();
            builder.Services.AddSingleton<LessonIteratorExecutor>();
            builder.Services.AddSingleton<LessonCreatingExecutor>();

            return builder;
        }

        public static WebApplicationBuilder AddSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

            return builder;
        }

        public static WebApplicationBuilder AddPublishers(this WebApplicationBuilder builder)
        {
            string coreUrl = builder.Configuration["PublisherURL"] ?? "https://localhost:5004";

            builder.Services.AddHttpClient<PublisherClient>()
                .AddTypedClient((httpClient, sp) => new PublisherClient(coreUrl, httpClient));

            return builder;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }

        public static WebApplicationBuilder AddPrimumContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PrimumContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPrimumContext>(provider =>
                provider.GetRequiredService<PrimumContext>());

            return builder;
        }
    }
}
