using DataNotifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.BackgroundWorkers.Executors
{
    public class LessonIteratorExecutor(IServiceScopeFactory _serviceScopeFactory)
    {
        public async Task Execute(ILogger? logger = null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IPrimumContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            var jitsiService = new JitsiLinkCreationService();

            var lessonsForIteration = context.Set<Lesson>()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(l => l.Status == LessonStatus.Warned)
                .Where(l => l.DateTime <= DateTime.Now.AddMinutes(30))
                .ToArray();

            foreach (var lesson in lessonsForIteration)
            {
                if (lesson.Abonement.Student.User.Cash >= lesson.Price &&
                    lesson.Abonement.AbonementStatus == AbonementStatus.Active)//Занятие произошло
                {
                    lesson.Abonement.Student.User.Cash -= lesson.Price;
                    lesson.Status = LessonStatus.Happened;

                    (string adminLink, string guestLink) tuple = jitsiService.CreateJitsiMeeting(
                        DateTime.Now.ToString() + lesson.AbonementId.ToString());
                    lesson.StudentLink = tuple.guestLink;
                    lesson.TeacherLink = tuple.adminLink;
                    await publisher.PublishAsync(new LessonNotification()
                    {
                        StudentName = lesson.Abonement.Student.User.DisplayName,
                        StudentUserId = lesson.Abonement.Student.User.Id,
                        TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                        TeacherUserId = lesson.Abonement.Course.TeacherId,
                        CourseName = lesson.Abonement.Course.Name,
                        AbonementId = lesson.Abonement.AbonementId,
                        LessonId = lesson.LessonId,
                        DateTime = lesson.DateTime,
                        StudentLink = tuple.guestLink,
                        TeacherLink = tuple.adminLink
                    });
                    logger?.LogInformation($"Lesson {lesson.LessonId} happened successfully");
                }
                else if (lesson.Abonement.Student.User.Cash < lesson.Price &&
                    lesson.Abonement.AbonementStatus == AbonementStatus.Active)//Занятие не оплачено и удаляется
                {
                    lesson.Abonement.AbonementStatus = AbonementStatus.Deleted;
                    lesson.Status = LessonStatus.Missed;
                    context.Set<AbonementShedule>().RemoveRange(lesson.Abonement.AbonementShedules);
                    await publisher.PublishAsync(new LessonFailureNotification()
                    {
                        StudentName = lesson.Abonement.Student.User.DisplayName,
                        StudentUserId = lesson.Abonement.Student.User.Id,
                        TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                        TeacherUserId = lesson.Abonement.Course.TeacherId,
                        CourseName = lesson.Abonement.Course.Name,
                        AbonementId = lesson.Abonement.AbonementId,
                        LessonId = lesson.LessonId,
                        DateTime = lesson.DateTime
                    });
                    logger?.LogInformation($"Lesson {lesson.LessonId} not happened and deleted abonement");
                }
                else //Занятие пропущено по сторонним причинам
                {
                    lesson.Status = LessonStatus.Missed;
                    logger?.LogInformation($"Lesson {lesson.LessonId} missed");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
