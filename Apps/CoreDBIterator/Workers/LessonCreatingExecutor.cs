using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using SharedCoreBusinessLogic;

namespace CoreDBIterator.Workers
{
    public class LessonCreatingExecutor(IServiceScopeFactory _serviceScopeFactory, ILogger<LessonCreatingExecutor> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Lesson creation running at: {time}", DateTimeOffset.Now);
                // Сбой одной итерации (недоступна платёжка, база и т.п.) не должен останавливать хост:
                // исключение из ExecuteAsync по умолчанию гасит все воркеры сервиса
                try
                {
                    await Action();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{Worker} iteration failed", nameof(LessonCreatingExecutor));
                }
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseIterator>();
            var lessonBuilder = scope.ServiceProvider.GetRequiredService<LessonBuilder>();

            var availableForProlongation = await context.AbonementShedules()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Lessons)
                .Include(x => x.TeacherShedule)
                .Where(s => s.LastIteration.AddDays(7) <= DateTime.UtcNow)
                .ToArrayAsync();

            if (availableForProlongation.Length != 0) 
            {
                logger.LogInformation($"Found {availableForProlongation.Length} abonement schedules available for creating lessons.");
            }
            else
            {
                logger.LogInformation("No abonement schedules available for creating lessons found.");
            }

            foreach (var s in availableForProlongation)
            {
                if (AvailabilityExpressions.IsAbonementAlive.Compile()(s.Abonement))
                {
                    var lesson = await lessonBuilder.Build(s, SlotPickPolicy.NextWeek, SlotConflictPolicy.SkipWeek);
                    await context.AddAsync(lesson);
                    logger?.LogInformation($"Created lesson for {lesson.AbonementId} at {lesson.DateTime}");
                }
                else
                {
                    s.LastIteration = DateTime.UtcNow;
                    logger?.LogInformation($"Lesson creation iterrupted due to unalive abonement");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
