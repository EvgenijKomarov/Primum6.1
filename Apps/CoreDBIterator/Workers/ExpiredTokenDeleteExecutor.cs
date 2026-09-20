using CoreDBModel.Models;
using CoreDBModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBIterator.Workers
{
    public class ExpiredTokenDeleteExecutor(IServiceProvider serviceProvider, ILogger<ExpiredTokenDeleteExecutor> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Lesson creation running at: {time}", DateTimeOffset.Now);
                await Action();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseIterator>();

            VerificationToken[] expiredVerificationTokens = context.VerificationTokens(false)
                .Where(x => x.LifeTime < DateTime.UtcNow)
                .ToArray();

            if (expiredVerificationTokens.Length == 0)
            {
                logger.LogInformation($"Found {expiredVerificationTokens.Length} expired tokens for delete");
            }
            else
            {
                logger.LogInformation("No expired tokens found at: {time}", DateTimeOffset.Now);
            }

            foreach(var token in expiredVerificationTokens)
            {
                await context.RemoveAsync(token);
                logger.LogInformation($"Token {token.Token} ({token.Meaning.ToString()}) was deleted");
            }
            await context.SaveChangesAsync();
        }
    }
}
