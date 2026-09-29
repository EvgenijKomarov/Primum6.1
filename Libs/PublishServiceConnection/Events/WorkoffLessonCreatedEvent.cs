using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Events
{
    public class WorkoffLessonCreatedEvent : IChatBotNotification, IMailNotification, ICommonNotification
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
                [TeacherUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие-отработка с {StudentName} было создано на {DateTime.Add(TeacherTimezoneOffset)}"
            };
        }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            list.Add(new Email
            {
                Address = TeacherEmail,
                MailTitle = "Уведомление о создании занятия-отработки",
                Data = new()
                {
                    ["body"] = $"Занятие-отработка с {StudentName} было создано на {DateTime.Add(TeacherTimezoneOffset)}"
                },
                EmailTemplate = EmailTemplate.InfoEmail
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие-отработка с {StudentName} было создано на {DateTime.Add(TeacherTimezoneOffset)}"
            };
        }
    }
}
