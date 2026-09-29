using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System.Diagnostics;
using System.Text.Json;

namespace PublishServiceConnection.Events
{
    public class LessonReadyEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required string TeacherEmail { get; set; }

        public required string StudentEmail { get; set; }

        public required DateTime DateTime { get; set; }

        public required string TeacherLink { get; set; }

        public required string StudentLink { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие с {StudentName} состоится через 30 минут!\nОно будет доступно по ссылке: {TeacherLink}",
                [StudentUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие по {CourseName} состоится через 30 минут!\nОно будет доступно по ссылке: {StudentLink}"
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
                    ["body"] = $"Занятие с {StudentName} состоится через 30 минут!\nОно будет доступно по ссылке:",
                    ["link"] = TeacherLink
                },
                EmailTemplate = EmailTemplate.LessonReadyEmail
            });
            list.Add(new Email
            {
                Address = StudentEmail,
                MailTitle = "Уведомление о будущем занятии",
                Data = new()
                {
                    ["body"] = $"Занятие по {CourseName} состоится через 30 минут!\nОно будет доступно по ссылке:",
                    ["link"] = StudentLink
                },
                EmailTemplate = EmailTemplate.LessonReadyEmail
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие с {StudentName} состоится через 30 минут!\n Ссылка доступна в личном кабинете",
                [StudentUserId] = $"Занятие по {CourseName} состоится через 30 минут!\n Ссылка доступна в личном кабинете"
            };
        }
    }
}
