using PublishServiceConnection.Abstractions;
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

        public required int TeacherTimezoneOffset { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public string MailTitle => "Уведомление о создании занятия-отработки";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие-отработка с {StudentName} было создано на {DateTime.AddHours(TeacherTimezoneOffset)}"
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие-отработка с {StudentName} было создано на {DateTime.AddHours(TeacherTimezoneOffset)}"
            };
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие-отработка с {StudentName} было создано на {DateTime.AddHours(TeacherTimezoneOffset)}"
            };
        }
    }
}
