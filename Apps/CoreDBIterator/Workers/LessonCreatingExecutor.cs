using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
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
                await Action();
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();
            var lessonBuilder = scope.ServiceProvider.GetRequiredService<LessonBuilder>();

            var availableForProlongation = await context.Set<AbonementShedule>()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Lessons)
                .Include(x => x.TeacherShedule)
                .Where(s => s.LastIteration.AddDays(7) <= DateTime.UtcNow)
                .Where(s => s.Abonement.AbonementStatus == AbonementStatus.Active)
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
                    context.Set<Lesson>().Add(lesson);
                    logger?.LogInformation($"Created lesson with Id: {lesson.Id} for {lesson.AbonementId} at {lesson.DateTime}");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
