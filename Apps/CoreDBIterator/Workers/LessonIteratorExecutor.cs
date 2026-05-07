using Common.Utilities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PublishServiceConnection;
using PublishServiceConnection.Events;

namespace CoreDBIterator.Workers
{
    public class LessonIteratorExecutor(IServiceScopeFactory _serviceScopeFactory, ILogger<LessonIteratorExecutor> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Lesson iteration running at: {time}", DateTimeOffset.Now);
                await Action();
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<PublisherService>();
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

            if (lessonsForIteration.Length != 0)
            {
                logger.LogInformation($"Found {lessonsForIteration.Length} lessons available for iteration.");
            }
            else
            {
                logger.LogInformation("No lessons available for iteration found.");
            }

            foreach (var lesson in lessonsForIteration)
            {
                if (lesson.Abonement.Student.Cash >= lesson.Price &&
                    AvailabilityExpressions.IsAbonementAvailable.Compile()(lesson.Abonement))//Занятие произошло
                {
                    lesson.Abonement.Student.Cash -= lesson.Price;
                    //lesson.Abonement.Course.Teacher.User.Cash += (long)(lesson.Price * lesson.Abonement.Course.Teacher.Rank.EarningMultiplier);
                    lesson.Status = LessonStatus.Happened;

                    (string adminLink, string guestLink) tuple = jitsiService.CreateJitsiMeeting(
                        DateTime.Now.ToString() + lesson.AbonementId.ToString());
                    lesson.StudentLink = tuple.guestLink;
                    lesson.TeacherLink = tuple.adminLink;
                    await publisher.Push(new LessonReadyEvent()
                    {
                        StudentName = lesson.Abonement.Student.User.DisplayName,
                        StudentUserId = lesson.Abonement.Student.User.Id,
                        TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                        TeacherUserId = lesson.Abonement.Course.TeacherId,
                        CourseName = lesson.Abonement.Course.Name,
                        AbonementId = lesson.Abonement.Id,
                        LessonId = lesson.Id,
                        DateTime = lesson.DateTime,
                        StudentLink = tuple.guestLink,
                        TeacherLink = tuple.adminLink
                    });
                    logger?.LogInformation($"Lesson {lesson.Id} happened successfully");
                }
                else if (lesson.Abonement.Student.Cash < lesson.Price &&
                    AvailabilityExpressions.IsAbonementAvailable.Compile()(lesson.Abonement))//Занятие не оплачено и удаляется
                {
                    lesson.Status = LessonStatus.Missed;
                    var notification = new LessonFailureEvent()
                    {
                        StudentName = lesson.Abonement.Student.User.DisplayName,
                        StudentUserId = lesson.Abonement.Student.User.Id,
                        TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                        TeacherUserId = lesson.Abonement.Course.TeacherId,
                        CourseName = lesson.Abonement.Course.Name,
                        AbonementId = lesson.Abonement.Id,
                        LessonId = lesson.Id,
                        DateTime = lesson.DateTime
                    };
                    await publisher.Push(notification);
                    logger?.LogInformation($"Lesson {lesson.Id} not happened");
                }
                else //Занятие пропущено по сторонним причинам
                {
                    logger?.LogInformation($"Lesson {lesson.Id} missed due to non-active abonement");
                    context.Set<Lesson>().Remove(lesson);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
