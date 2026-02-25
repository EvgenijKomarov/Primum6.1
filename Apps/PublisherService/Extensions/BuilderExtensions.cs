using MassTransit;
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
                builder.Services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(rabbitMqConnection, h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        // Именование очередей: по умолчанию "namespace:MessageType"
                        cfg.ConfigureEndpoints(context);
                    });
                });
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
