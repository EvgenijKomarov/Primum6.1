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
                logger.LogInformation("Expired token cleanup running at: {time}", DateTimeOffset.Now);
                // Сбой одной итерации (недоступна платёжка, база и т.п.) не должен останавливать хост:
                // исключение из ExecuteAsync по умолчанию гасит все воркеры сервиса
                try
                {
                    await Action();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{Worker} iteration failed", nameof(ExpiredTokenDeleteExecutor));
                }
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
                logger.LogInformation("No expired tokens found at: {time}", DateTimeOffset.Now);
            }
            else
            {
                logger.LogInformation("Found {Count} expired tokens for delete", expiredVerificationTokens.Length);
            }

            foreach(var token in expiredVerificationTokens)
            {
                await context.RemoveAsync(token);
                // Значение токена — код подтверждения, в лог пишем только id
                logger.LogInformation("Token {TokenId} ({Meaning}) was deleted", token.Id, token.Meaning);
            }
            await context.SaveChangesAsync();
        }
    }
}
