using AnonimousTokenProducer;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Inputs;
using BotCore.Engine.Entities.Outputs;
using BotCore.Engine.Middlewares;
using BotCore.Engine.Nodes.EndpointNodes;
using BotCore.Services;
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
            builder.Services.AddSingleton(sp => 
                new EasyBotEngine<CommandInput, DataBuffer, OutputMessage>(
                    sp,
                    sp.GetService<ILogger<EasyBotEngine<CommandInput, DataBuffer, OutputMessage>>>()
                ));
            return builder;
        }

        public static WebApplicationBuilder AddNodes(this WebApplicationBuilder builder)
        {
            builder.Services.AddEngineEndpointNode<PlainTextNode, DataBuffer, OutputMessage>();
            builder.Services.AddEngineEndpointNode<ProfileNode, DataBuffer, OutputMessage>();

            return builder;
        }

        public static WebApplicationBuilder AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.Services.AddEngineMiddleware<AuthentificationMiddleware, DataBuffer, OutputMessage>();

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
            builder.Services.AddScoped<InOutConverter>();

            builder.Services.AddTransient<SignTokenWorker>();


            return builder;
        }
    }
}
