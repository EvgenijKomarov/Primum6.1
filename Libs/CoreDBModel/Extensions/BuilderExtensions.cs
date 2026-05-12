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
        public static IServiceCollection AddCoreContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PrimumContext>(options =>
                options.UseNpgsql(connectionString, npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(PrimumContext).Assembly.FullName);
                    npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Авто-ретрай при кратковременных сбоях
                    npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));

            return services;
        }
    }
}
