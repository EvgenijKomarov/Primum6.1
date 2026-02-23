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
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
