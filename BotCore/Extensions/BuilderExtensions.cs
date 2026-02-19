using BotCore.Entities;
using BotCore.Entities.Inputs;
using BotCore.Entities.Outputs;
using BotCore.Nodes.EndpointNodes;
using BotCore.Services.Iterators;
using CoreConnection;
using Engine;
using Engine.Extensions;
using Serilog;

namespace BotCore.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddClients(this WebApplicationBuilder builder)
        {
            string coreUrl = builder.Configuration["Api:CoreURL"] ?? "https://localhost:5001";

            builder.Services.AddHttpClient<AdminClient>()
                .AddTypedClient((httpClient, sp) => new AdminClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<StudentClient>()
                .AddTypedClient((httpClient, sp) => new StudentClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<UserClient>()
                .AddTypedClient((httpClient, sp) => new UserClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<PublicClient>()
                .AddTypedClient((httpClient, sp) => new PublicClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<TeacherClient>()
                .AddTypedClient((httpClient, sp) => new TeacherClient(coreUrl, httpClient));

            return builder;
        }

        public static WebApplicationBuilder AddBotEngine(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<EasyBotEngine<CommandInput, DataBuffer, OutputMessage>>(sp => 
                new EasyBotEngine<CommandInput, DataBuffer, OutputMessage>(
                    sp,
                    sp.GetService<ILogger<EasyBotEngine<CommandInput, DataBuffer, OutputMessage>>>()
                ));
            return builder;
        }

        public static WebApplicationBuilder AddNodes(this WebApplicationBuilder builder)
        {
            builder.Services.AddEngineEndpointNode<PlainTextNode, DataBuffer, OutputMessage>();

            return builder;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<BotIterator>();


            return builder;
        }
    }
}
