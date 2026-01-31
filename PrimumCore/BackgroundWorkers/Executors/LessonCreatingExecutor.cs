using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.BackgroundWorkers.Executors
{
    public class LessonCreatingExecutor(IServiceScopeFactory _serviceScopeFactory)
    {
        public async Task Execute(ILogger? logger = null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IPrimumContext>();
            var datetimeService = scope.ServiceProvider.GetRequiredService<ConverterToDateTimeService>();

            var availableForProlongation = await context.Set<AbonementShedule>()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Include(x => x.TeacherShedule)
                .Where(s => s.LastIteration.AddDays(7) <= DateTime.Now)
                .ToArrayAsync();

            foreach (var s in availableForProlongation)
            {
                var freeDateTime = datetimeService.GetNextSuitableDateThisWeek(s.TeacherShedule.DayOfWeek, s.TeacherShedule.Time);

                s.LastIteration = freeDateTime;
                logger?.LogInformation($"Set LastIterationTime of {s.AbonementSheduleId} for {freeDateTime}");
                var lesson = new Lesson()
                {
                    AbonementId = s.Abonement.AbonementId,
                    DateTime = freeDateTime,
                    Price = s.Abonement.Course.FreeLessons > s.Abonement.Lessons.Count() ? 0 : s.Abonement.PricePerLesson,
                    Status = LessonStatus.Waiting
                };
                context.Set<Lesson>().Add(lesson);
                logger?.LogInformation($"Created lesson with Id: {lesson.LessonId} for {lesson.AbonementId} at {lesson.DateTime}");
            }

            await context.SaveChangesAsync();
        }
    }
}
