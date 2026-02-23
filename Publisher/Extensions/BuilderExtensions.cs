using MassTransit;
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
                builder.Services.AddMassTransit(x =>
                {
                    x.SetSnakeCaseEndpointNameFormatter();//snake формат для очередей

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(builder.Configuration["RabbitMQ:Host"], h =>
                        {
                            h.Username(builder.Configuration["RabbitMQ:User"]);
                            h.Password(builder.Configuration["RabbitMQ:Password"]);
                        });

                        // Настройка топологии
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
