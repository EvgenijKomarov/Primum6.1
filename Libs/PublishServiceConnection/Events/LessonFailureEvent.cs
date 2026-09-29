using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
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
    public class LessonFailureEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required string TeacherEmail { get; set; }

        public required string StudentEmail { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{BoolRes._false}{Emoticons.Lesson}Занятие с {StudentName} не состоится в связи с невозможностью оплаты",
                [StudentUserId] = $"{BoolRes._false}{Emoticons.Lesson}Занятие по {CourseName} не состоится в связи с невозможностью оплаты"
            };
        }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            list.Add(new Email
            {
                Address = TeacherEmail,
                MailTitle = "Уведомление о будущем занятии",
                Data = new()
                {
                    ["body"] = $"Занятие с {StudentName} не состоится в связи с невозможностью оплаты"
                },
                EmailTemplate = EmailTemplate.InfoEmail
            });
            list.Add(new Email
            {
                Address = StudentEmail,
                MailTitle = "Уведомление о будущем занятии",
                Data = new()
                {
                    ["body"] = $"Занятие по {CourseName} не состоится в связи с невозможностью оплаты"
                },
                EmailTemplate = EmailTemplate.InfoEmail
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие с {StudentName} не состоится в связи с невозможностью оплаты",
                [StudentUserId] = $"Занятие по {CourseName} не состоится в связи с невозможностью оплаты"
            };
        }
    }
}
