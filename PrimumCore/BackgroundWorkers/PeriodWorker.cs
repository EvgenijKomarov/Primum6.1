namespace PrimumCore.BackgroundWorkers
{
    public class PeriodWorker(
        string name,
        Task task,
        TimeSpan timeSpan,
        ILogger? logger = null
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger?.LogInformation($"{name} is starting");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    logger?.LogInformation($"{name} is iterating database...");

                    await task;
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, $"{name} exception");
                }

                await Task.Delay(timeSpan, stoppingToken);
            }
        }
    }
}
