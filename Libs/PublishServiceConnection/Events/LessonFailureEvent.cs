using PublishServiceConnection.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

namespace PublishServiceConnection.Events
{
    public class LessonFailureEvent : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public string MailTitle => "Уведомление о будущем занятии";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                [StudentUserId] = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты"
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                [StudentUserId] = $"Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты"
            };
        }
    }
}
