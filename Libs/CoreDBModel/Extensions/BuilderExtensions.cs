using CoreDBModel.Models;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Extensions
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddCoreContext(this IServiceCollection services)
        {
            var url = Environment.GetEnvironmentVariable("COREDB_URL") ?? throw new ArgumentNullException("Missing env variable");

            services.AddScoped<DatabaseIterator>(sp =>
            {
                var options = new DbContextOptionsBuilder<PrimumContext>()
                    .UseNpgsql(url, npgsql =>
                    {
                        npgsql.MigrationsAssembly(typeof(PrimumContext).Assembly.FullName);
                        npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    })
                    .Options;

                var context = new PrimumContext(options);
                return new DatabaseIterator(context);
            });

            return services;
        }
    }
}
