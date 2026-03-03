using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Pushables;
using Pushables.Events;

namespace CoreDBIterator.Workers
{
    public class LessonWarningExecutor(IServiceScopeFactory _serviceScopeFactory, ILogger<LessonWarningExecutor> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Lesson warning running at: {time}", DateTimeOffset.Now);
                await Action();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<PublisherService>();

            var lessonsForPreparation = context.Set<Lesson>()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(l => l.Status == LessonStatus.Waiting)
                .Where(l => l.DateTime <= DateTime.Now.AddDays(1))
                .ToArray();

            if (lessonsForPreparation.Length != 0)
            {
                logger.LogInformation($"Found {lessonsForPreparation.Length} lessons available for warn.");
            }
            else
            {
                logger.LogInformation("No lessons available for warn found.");
            }

            foreach (var lesson in lessonsForPreparation)
            {
                lesson.Status = LessonStatus.Warned;
                await publisher.Push(new LessonPreparationEvent()
                {
                    StudentName = lesson.Abonement.Student.User.DisplayName,
                    StudentUserId = lesson.Abonement.Student.User.Id,
                    TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                    TeacherUserId = lesson.Abonement.Course.TeacherId,
                    CourseName = lesson.Abonement.Course.Name,
                    AbonementId = lesson.Abonement.AbonementId,
                    LessonId = lesson.LessonId,
                    DateTime = lesson.DateTime,
                    IsEnoughMoney = lesson.Abonement.Student.User.Cash >= lesson.Price
                });
                logger.LogInformation($"Lesson {lesson.LessonId} warned");
            }

            await context.SaveChangesAsync();
        }
    }
}
