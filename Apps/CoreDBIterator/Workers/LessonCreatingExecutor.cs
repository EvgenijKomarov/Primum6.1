using Common.Utilities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

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
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        protected async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();
            var datetimeService = scope.ServiceProvider.GetRequiredService<ConverterToDateTimeService>();

            var availableForProlongation = await context.Set<AbonementShedule>()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Include(x => x.TeacherShedule)
                .Where(s => s.LastIteration.AddDays(7) <= DateTime.Now)
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
                var freeDateTime = datetimeService.GetNextSuitableDateThisWeek(s.TeacherShedule.DayOfWeek, s.TeacherShedule.Time);

                s.LastIteration = freeDateTime;
                logger.LogInformation($"Set LastIterationTime of {s.AbonementSheduleId} for {freeDateTime}");
                if (AvailabilityExpressions.IsAbonementAlive.Compile()(s.Abonement))
                {
                    var lesson = new Lesson()
                    {
                        AbonementId = s.Abonement.AbonementId,
                        DateTime = freeDateTime,
                        Price = s.Abonement.FreeLessons > s.Abonement.Lessons.Count() ? 0 : s.Abonement.PricePerLesson,
                        Status = LessonStatus.Waiting
                    };
                    context.Set<Lesson>().Add(lesson);
                    logger?.LogInformation($"Created lesson with Id: {lesson.LessonId} for {lesson.AbonementId} at {lesson.DateTime}");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
