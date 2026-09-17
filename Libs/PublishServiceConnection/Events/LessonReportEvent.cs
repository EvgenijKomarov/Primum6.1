using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Events
{
    public class LessonReportEvent: IChatBotNotification, IMailNotification
    {
        public required IEnumerable<int> AllowedAdminIds { get; set; }

        public required IEnumerable<string> AllowedAdminEmails { get; set; }

        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required string ReportStatus { get; set; }

        public required DateTime DateTime { get; set; }

        public EmailTemplate Template { get; } = EmailTemplate.InfoEmail;

        public string MailTitle => "Репорт занятия";

        private string mes => $"По занятию в {DateTime.ToString("HH:mm dd.MM.yyyy")}(UTC) между учеником {StudentName} и преподавателем {TeacherName} поступил репорт по теме {ReportStatus}";

        public Dictionary<int, string> ToChatBotNotifications()
        {
            var dict = new Dictionary<int, string>();
            foreach (var id in AllowedAdminIds) dict.Add(id, mes);
            return dict;
        }

        public Dictionary<string, string> ToMailNotifications()
        {
            var dict = new Dictionary<string, string>();
            foreach (var email in AllowedAdminEmails) dict.Add(email, mes);
            return dict;
        }
    }
}
