using PublishServiceConnection.Abstractions;
using Resourses;
using System.Diagnostics;
using System.Text.Json;

namespace PublishServiceConnection.Events
{
    public class LessonReadyEvent : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public required string TeacherLink { get; set; }

        public required string StudentLink { get; set; }

        public string MailTitle => "Уведомление о будущем занятии";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {TeacherLink}",
                [StudentUserId] = $"{BoolRes._true}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {StudentLink}"
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {TeacherLink}",
                [StudentUserId] = $"Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {StudentLink}"
            };
        }
    }
}
