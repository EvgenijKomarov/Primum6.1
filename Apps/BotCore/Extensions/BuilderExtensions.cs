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
using SignServiceConnection;
using SignServiceConnection.Models;
using SolutionConfiguration;

namespace BotCore.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddClients(this WebApplicationBuilder builder, string coreUrl)
        {
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

        public static WebApplicationBuilder AddSignService(this WebApplicationBuilder builder, string signServiceUrl)
        {
            builder.Services.AddHttpClient<SignServiceClient>()
                .AddTypedClient((httpClient, sp) => new SignServiceClient(signServiceUrl, httpClient));

            return builder;
        }

        public static WebApplicationBuilder AddBotEngine(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(sp => 
                new EasyBotEngine<CommandEngineInput, DataBuffer, EngineOutputMessage>(
                    sp,
                    sp.GetService<ILogger<EasyBotEngine<CommandEngineInput, DataBuffer, EngineOutputMessage>>>()
                ));
            return builder;
        }

        public static WebApplicationBuilder AddNodes(this WebApplicationBuilder builder)
        {
            builder.Services.AddEngineEndpointNode<PlainTextNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<ProfileNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentProfileNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherProfileNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherFutureLessonsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentFutureLessonsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAllLessonsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentSheduleDeleteNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementSheduleNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementShedulesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementDeleteNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentAbonementActivateDeactivateNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentBoughtPromocodesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentSubscribeCourseNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentExploreTeacherShedulesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentExploreCoursesByThemeNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<StudentExploreThemesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherAllLessonsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherAbonementsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherCoursesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<TeacherShedulesNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<AdminProfileNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<AdminIncidentsNode, DataBuffer, EngineOutputMessage>();
            builder.Services.AddEngineEndpointNode<AdminSolveIncidentNode, DataBuffer, EngineOutputMessage>();

            return builder;
        }

        public static WebApplicationBuilder AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.Services.AddEngineMiddleware<AuthentificationMiddleware, DataBuffer, EngineOutputMessage>();

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

            builder.Services.AddTransient<ChatSignTokenWorker>();
            builder.Services.AddHttpClient<ConfigurationClient>();

            return builder;
        }
    }
}
