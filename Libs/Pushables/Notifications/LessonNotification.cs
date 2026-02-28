using Pushables.Entities;
using Resourses;
using System.Text.Json;

namespace Pushables.Notifications
{
    public class LessonNotification : IChatBotNotification, IMailNotification
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

        public IEnumerable<ChatBotNotification> GetChatBotNotifications()
        {
            return [
                new ChatBotNotification
                {
                    UserId = TeacherUserId,
                    Text = $"{BoolRes._true}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {TeacherLink}",
                },
                new ChatBotNotification
                {
                    UserId = StudentUserId,
                    Text = $"{BoolRes._true}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {StudentLink}",
                }
            ];
        }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = TeacherUserId,
                    Title = "Уведомление о будущем занятии",
                    Text = $"Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {TeacherLink}",
                },
                new MailNotification
                {
                    UserId = StudentUserId,
                    Title = "Уведомление о будущем занятии",
                    Text = $"Занятие в {DateTime.ToString("HH:mm")} состоится совсем скоро!\nОно будет доступно по ссылке: {StudentLink}",
                }
            ];
        }
    }
}
