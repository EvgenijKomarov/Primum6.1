using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PrimumContext>
    {
        public PrimumContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PrimumContext>();
            // Временная строка подключения для генерации миграций
            var connectionString = "Host=localhost;Port=5432;Database=CoreDB;Username=postgres;Password=password;SSL Mode=Disable;Include Error Detail=true;";
            optionsBuilder.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(PrimumContext).Assembly.FullName);
                npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Авто-ретрай при кратковременных сбоях
                npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            return new PrimumContext(optionsBuilder.Options);
        }
    }
}
