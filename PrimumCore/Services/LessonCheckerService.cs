using DataNotifications;
using Microsoft.Extensions.Logging;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services
{
    public class LessonCheckerService(
        IPrimumContext context,
        JitsiLinkCreationService _jitsiService,
        CoreNotificationService _notificationService,
        ILogger<LessonCheckerService>? _logger = null
        )
    {
        private Timer _timer;

        public void Run()
        {
            _logger?.LogInformation("LessonCheckerService is running...");
            _timer = new Timer(async _ => await IterateAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public async Task IterateAsync()
        {
            _logger?.LogInformation("LessonCheckerService is iterating database...");
            try
            {
                //prep notif
                var lessonsForPreparationNotification = context.Set<Lesson>()
                    .Where(l => l.Status == LessonStatus.Waiting)
                    .Where(l => l.DateTime <= DateTime.Now.AddDays(1))
                    .ToArray();

                if (lessonsForPreparationNotification.Length > 0)
                {
                    _logger?.LogInformation($"LessonCheckerService found lessons for prepare notifications: " +
                        $"{lessonsForPreparationNotification.Select(x => x.LessonId).ToArray()}");
                }
                else {
                    _logger?.LogInformation($"LessonCheckerService found no lessons for prepare notifications");
                }

                foreach (var lesson in lessonsForPreparationNotification)
                {
                    lesson.Status = LessonStatus.Warned;
                    await _notificationService.PublishAsync(new LessonPreparationNotification()
                    {
                        DateTime = lesson.DateTime,
                        StudentUserId = lesson.Abonement.StudentId,
                        TeacherUserId = lesson.Abonement.Course.TeacherId,
                        IsEnoughPaidLessons = lesson.Abonement.PaidLessons > 0
                    });
                    _logger?.LogInformation($"Lesson {lesson.LessonId} warned");
                }

                //notif
                var lessonsForNotification = context.Set<Lesson>()
                    .Where(l => l.Status == LessonStatus.Warned)
                    .Where(l => l.DateTime <= DateTime.Now.AddMinutes(30))
                    .ToArray();

                if (lessonsForNotification.Length > 0)
                {
                    _logger?.LogInformation($"LessonCheckerService found lessons for notifications: " +
                        $"{lessonsForNotification.Select(x => x.LessonId).ToArray()}");
                }
                else
                {
                    _logger?.LogInformation($"LessonCheckerService found no lessons for notifications");
                }

                foreach (var lesson in lessonsForNotification)
                {
                    if (lesson.Abonement.PaidLessons > 0 && 
                        lesson.Abonement.AbonementStatus == AbonementStatus.Active)//Занятие произошло
                    {
                        lesson.Abonement.PaidLessons -= 1;
                        lesson.Status = LessonStatus.Happened;

                        (string adminLink, string guestLink) tuple = _jitsiService.CreateJitsiMeeting(
                            DateTime.Now.ToString() + lesson.AbonementId.ToString());
                        await _notificationService.PublishAsync(new LessonNotification()
                        {
                            StudentLink = tuple.guestLink,
                            TeacherLink = tuple.adminLink,
                            DateTime = lesson.DateTime,
                            StudentUserId = lesson.Abonement.StudentId,
                            TeacherUserId = lesson.Abonement.Course.TeacherId
                        });
                        _logger?.LogInformation($"Lesson {lesson.LessonId} happened successfully");
                    }
                    else if (lesson.Abonement.PaidLessons <= 0 && 
                        lesson.Abonement.AbonementStatus == AbonementStatus.Active)//Занятие не оплачено и удаляется
                    {
                        lesson.Abonement.AbonementStatus = AbonementStatus.Deleted;
                        lesson.Status = LessonStatus.Missed;
                        context.Set<AbonementShedule>().RemoveRange(lesson.Abonement.AbonementShedules);
                        await _notificationService.PublishAsync(new LessonFailureNotification()
                        {
                            CourseName = lesson.Abonement.Course.Name,
                            StudentUserId = lesson.Abonement.StudentId,
                            TeacherUserId = lesson.Abonement.Course.TeacherId,
                            DateTime = lesson.DateTime
                        });
                        _logger?.LogInformation($"Lesson {lesson.LessonId} not happened and deleted");
                    }
                    else //Занятие пропущено по сторонним причинам
                    {
                        lesson.Status = LessonStatus.Missed;
                        _logger?.LogInformation($"Lesson {lesson.LessonId} missed");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, $"LessonCheckerService failed to iterate");
            }
        }
    }
}
