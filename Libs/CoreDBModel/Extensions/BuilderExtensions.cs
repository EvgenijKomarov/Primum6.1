using CoreDBModel.Models;
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

            services.AddDbContext<PrimumContext>(options =>
                options.UseNpgsql(url, npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(PrimumContext).Assembly.FullName);
                    npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Авто-ретрай при кратковременных сбоях
                    npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));

            return services;
        }
    }
}
