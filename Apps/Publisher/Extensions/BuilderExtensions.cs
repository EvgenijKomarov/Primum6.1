using Publisher.Services;
using Serilog;

namespace Publisher.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddPublisher(this WebApplicationBuilder builder)
        {
            if (builder.Configuration.GetValue<bool>("RabbitMQ:IsFake"))
            {
                builder.Services.AddScoped<IPublisher, FakePublisher>();
            }
            else
            {
                builder.Services.AddScoped<IPublisher, RabbitMQEventPublisher>();
            }
            return builder;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }
    }
}
