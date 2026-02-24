using Publisher.Services;
using Serilog;

namespace Publisher.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddPublisher(this WebApplicationBuilder builder, string rabbitMqConnection)
        {
            if (builder.Configuration.GetValue<bool>("IsFakePublisherOn"))
            {
                builder.Services.AddScoped<IPublisher, FakePublisher>();
            }
            else
            {
                builder.Services.AddScoped<IPublisher, RabbitMQEventPublisher>(sp => new RabbitMQEventPublisher(rabbitMqConnection));
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
