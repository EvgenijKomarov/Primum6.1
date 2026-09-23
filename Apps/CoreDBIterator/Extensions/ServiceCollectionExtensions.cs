using Common.Utilities;
using CoreDBIterator.Workers;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Services;
using PaymentServiceConnection;
using PublishServiceConnection;
using SharedCoreBusinessLogic;

namespace CoreDBIterator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Вынесено из Program.cs, чтобы автотесты поднимали хост с той же регистрацией, что и прод
        public static IServiceCollection AddCoreDbIteratorServices(this IServiceCollection services)
        {
            services.AddScoped<EarningCalculationService>();
            services.AddScoped<IQueryable<Lesson>>(sp => sp.GetRequiredService<DatabaseIterator>().Lessons());
            services.AddScoped<ConverterToDateTimeService>();
            services.AddScoped<LessonBuilder>();

            services.AddHttpClient<PublisherService>()
                    .AddTypedClient((httpClient, sp) => new PublisherService(httpClient));

            services.AddHttpClient<PaymentServiceClient>()
                    .AddTypedClient((httpClient, sp) => new PaymentServiceClient(httpClient));

            services.AddHostedService<LessonCreatingExecutor>();
            services.AddHostedService<LessonWarningExecutor>();
            services.AddHostedService<LessonIteratorExecutor>();
            services.AddHostedService<ExpiredTokenDeleteExecutor>();
            services.AddHostedService<TeacherProfileRefreshExecutor>();

            services.AddCoreContext();

            return services;
        }
    }
}
