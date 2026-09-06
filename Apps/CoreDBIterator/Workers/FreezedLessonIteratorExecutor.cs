using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBIterator.Workers
{
    public class FreezedLessonIteratorExecutor(IServiceScopeFactory _serviceScopeFactory, ILogger<LessonIteratorExecutor> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Freezed lesson iteration running at: {time}", DateTimeOffset.Now);
                await Action();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();

            var lessonsForIteration = context.Set<Lesson>()
                .Where(l => l.Status == LessonStatus.Freezed)
                .Where(l => l.DateTime <= DateTime.UtcNow.AddDays(1))
                .ToArray();

            if (lessonsForIteration.Length != 0)
            {
                logger.LogInformation($"Found {lessonsForIteration.Length} freezed lessons available for iteration.");
            }
            else
            {
                logger.LogInformation("No freezed lessons available for iteration found.");
            }

            foreach (var lesson in lessonsForIteration)
            {
                lesson.Status = LessonStatus.MissedDueToFreezing;
                logger?.LogInformation($"Lesson {lesson.Id} missed due to being freezed");
            }

            await context.SaveChangesAsync();
        }
    }
}
