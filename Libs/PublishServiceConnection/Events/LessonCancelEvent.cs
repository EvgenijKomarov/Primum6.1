using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace PublishServiceConnection.Events
{
    public class LessonCancelEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required TimeSpan TeacherTimezoneOffset { get; set; }

        public required string TeacherEmail { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Lesson}Занятие с {StudentName} в {DateTime.Add(TeacherTimezoneOffset)} отменено учеником"
            };
        }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            list.Add(new Email
            {
                Address = TeacherEmail,
                MailTitle = "Урок отменен",
                Data = new()
                {
                    ["body"] = $"Занятие с {StudentName} в {DateTime.Add(TeacherTimezoneOffset)} отменено учеником"
                },
                EmailTemplate = EmailTemplate.InfoEmail
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие с {StudentName} в {DateTime.Add(TeacherTimezoneOffset)} отменено учеником"
            };
        }
    }
}
