using ChatSigns;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Controllers;
using PrimumCore.Services.Iterators;
using PrimumCore.Services.Utilities;
using Pushables;
using Serilog;
using CoreDBModel.Extensions;
using Common.Utilities;
using SolutionConfiguration;

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

        public static WebApplicationBuilder AddPublishers(this WebApplicationBuilder builder, string publisherUrl)
        {
            builder.Services.AddHttpClient<PublisherService>()
                .AddTypedClient((httpClient, sp) => new PublisherService(publisherUrl, httpClient));

            return builder;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }

        public static WebApplicationBuilder AddContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddCoreContext(builder.Configuration.GetConnectionString("DefaultConnection"));

            return builder;
        }
    }
}
