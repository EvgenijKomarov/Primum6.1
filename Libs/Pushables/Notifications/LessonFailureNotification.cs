using Pushables.Entities;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pushables.Notifications
{
    public class LessonFailureNotification : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public IEnumerable<ChatBotNotification> GetChatBotNotifications()
        {
            return [
                new ChatBotNotification
                {
                    UserId = TeacherUserId,
                    Text = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                },
                new ChatBotNotification
                {
                    UserId = StudentUserId,
                    Text = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                }
            ];
        }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = TeacherUserId,
                    Text = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                },
                new MailNotification
                {
                    UserId = StudentUserId,
                    Text = $"{BoolRes._false}{Emoticons.Lesson}Занятие в {DateTime.ToString("HH:mm")} не состоится в связи с невозможностью оплаты",
                }
            ];
        }
    }
}
