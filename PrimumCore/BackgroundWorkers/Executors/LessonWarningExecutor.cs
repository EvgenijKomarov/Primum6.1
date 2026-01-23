using DataNotifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.BackgroundWorkers.Executors
{
    public class LessonWarningExecutor(IServiceScopeFactory _serviceScopeFactory)
    {
        public async Task Execute(ILogger? logger = null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IPrimumContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

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

            foreach (var lesson in lessonsForPreparation)
            {
                lesson.Status = LessonStatus.Warned;
                await publisher.PublishAsync(new LessonPreparationNotification()
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
                logger?.LogInformation($"Lesson {lesson.LessonId} warned");
            }

            await context.SaveChangesAsync();
        }
    }
}
