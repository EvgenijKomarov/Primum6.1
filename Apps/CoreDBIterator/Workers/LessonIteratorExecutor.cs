using Common.Utilities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using PaymentServiceConnection;
using PublishServiceConnection;
using PublishServiceConnection.Abstractions;
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
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseIterator>();
            var publisher = scope.ServiceProvider.GetRequiredService<PublisherService>();
            var paymentClient = scope.ServiceProvider.GetRequiredService<PaymentServiceClient>();
            var earningService = scope.ServiceProvider.GetRequiredService<EarningCalculationService>();
            var jitsiService = new JitsiLinkCreationService();

            var lessonsForIteration = context.Lessons()
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.Rank)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Lessons)
                .Where(l => l.Status == LessonStatus.Warned)
                .Where(l => l.DateTime <= DateTime.UtcNow.AddMinutes(30))
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
                try
                {
                    var studentCash = await paymentClient.GetStudentBalanceAsync(lesson.Abonement.Student.User.Id);
                    var teacher = lesson.Abonement.Course.Teacher;

                    if (!await paymentClient.IsTeacherReadyAsync(teacher.User.Id)) throw new Exception("Teacher not ready to process payments");

                    if (studentCash >= lesson.Price)//Занятие произошло
                    {
                        var teacherCash = earningService.CalculateEarningsToLesson(
                            lesson.Price,
                            teacher.ConvertionIndex,
                            teacher.Rank.EarningMultiplier,
                            lesson.Abonement.Lessons.Count(l => l.Price > 0 && l.Status == LessonStatus.Happened),
                            lesson.IsReferal
                            );

                        var isPaid = await paymentClient.ProcessLessonPaymentAsync(
                            lesson.Id,
                            lesson.Abonement.Student.User.Id,
                            teacher.User.Id,
                            teacherCash,
                            lesson.Price - teacherCash
                            );
                        if (!isPaid) throw new Exception("Lesson payment was rejected by payment service");
                        lesson.Status = LessonStatus.Happened;

                        (string adminLink, string guestLink) tuple = jitsiService.CreateJitsiMeeting(
                            DateTime.UtcNow.ToString() + lesson.AbonementId.ToString());
                        lesson.StudentLink = tuple.guestLink;
                        lesson.TeacherLink = tuple.adminLink;
                        lesson.TeacherEarning = teacherCash;
                        await NotifySafely(publisher, lesson.Id, new LessonReadyEvent()
                        {
                            StudentName = lesson.Abonement.Student.User.DisplayName,
                            StudentUserId = lesson.Abonement.Student.User.Id,
                            TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                            TeacherUserId = lesson.Abonement.Course.Teacher.User.Id,
                            TeacherEmail = lesson.Abonement.Course.Teacher.User.MailAdress,
                            StudentEmail = lesson.Abonement.Student.User.MailAdress,
                            CourseName = lesson.Abonement.Course.Name,
                            AbonementId = lesson.Abonement.Id,
                            LessonId = lesson.Id,
                            DateTime = lesson.DateTime,
                            StudentLink = tuple.guestLink,
                            TeacherLink = tuple.adminLink
                        });
                        logger?.LogInformation($"Lesson {lesson.Id} happened successfully");
                    }
                    else//Занятие не оплачено и удаляется
                    {
                        lesson.Status = LessonStatus.Missed;
                        await NotifySafely(publisher, lesson.Id, new LessonFailureEvent()
                        {
                            StudentName = lesson.Abonement.Student.User.DisplayName,
                            StudentUserId = lesson.Abonement.Student.User.Id,
                            TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                            TeacherEmail = lesson.Abonement.Course.Teacher.User.MailAdress,
                            StudentEmail = lesson.Abonement.Student.User.MailAdress,
                            TeacherUserId = lesson.Abonement.Course.Teacher.User.Id,
                            CourseName = lesson.Abonement.Course.Name,
                            AbonementId = lesson.Abonement.Id,
                            LessonId = lesson.Id,
                            DateTime = lesson.DateTime
                        });
                        if (lesson.IsWorkoff) lesson.Abonement.CancelledLessons += 1;
                        logger?.LogInformation($"Lesson {lesson.Id} not happened");
                    }
                }
                catch (Exception ex) 
                {
                    lesson.Status = LessonStatus.MissedDueToException;
                    if (lesson.IsWorkoff) lesson.Abonement.CancelledLessons += 1;
                    logger.LogError(ex, "Lesson {LessonId} iteration failed", lesson.Id);
                }
            }

            await context.SaveChangesAsync();
        }

        // Уведомление не входит в проведение урока: если сервис уведомлений недоступен,
        // уже проведённая оплата и статус урока всё равно должны сохраниться
        private async Task NotifySafely(PublisherService publisher, int lessonId, IPushable message)
        {
            try
            {
                await publisher.Push(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send notifications for lesson {LessonId}", lessonId);
            }
        }
    }
}
