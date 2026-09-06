using PublishServiceConnection.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Events
{
    public class LessonChangeStatusEvent : IChatBotNotification, ICommonNotification
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

        public required bool IsBecameFreezed { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            var status = IsBecameFreezed ? "Заморожен" : "Активен";
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Lesson}Занятие с {StudentName} в {DateTime.AddHours(TeacherTimezoneOffset)} " +
                    $"сменило статус на ({status})"
            };
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            var status = IsBecameFreezed ? "Заморожен" : "Активен";
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие с {StudentName} в {DateTime.AddHours(TeacherTimezoneOffset)} " +
                    $"сменило статус на ({status})"
            };
        }
    }
}
