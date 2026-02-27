using CoreDBModel.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBIterator.Workers
{
    public class DatabaseMigratorExecutor(IServiceProvider serviceProvider, ILogger<DatabaseMigratorExecutor> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting database migration check...");

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PrimumContext>();

            try
            {
                // Проверяем pending-миграции
                var pending = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pending.Any())
                {
                    logger.LogInformation("Applying {Count} pending migration(s): {Migrations}",
                        pending.Count(), string.Join(", ", pending));

                    await dbContext.Database.MigrateAsync(cancellationToken);

                    logger.LogInformation("Migrations applied successfully");
                }
                else
                {
                    logger.LogInformation("Database is up to date");
                }
            }
            catch (Exception ex) when (ex is SqlException or TimeoutException)
            {
                logger.LogError(ex, "Database migration failed: connection issue");
                throw; // Останавливаем приложение — БД критична
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database migration failed: {Error}", ex.Message);
                throw;
            }
        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
