using Pushables.Entities;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Notifications
{
    public class LessonGradedNotification : IChatBotNotification, IMailNotification
    {
        public required string StudentDisplayName;
        public required int StudentUserId;
        public required DateTime DateTime;
        public required string CourseName;
        public required int CourseId;
        public required string TeacherDisplayName;
        public required int TeacherUserId;
        public required float Grade;
        public required int EarnedCoins;

        public IEnumerable<ChatBotNotification> GetChatBotNotifications()
        {
            return [
                new ChatBotNotification
                {
                    UserId = StudentUserId,
                    Text = $"{Emoticons.Lesson}Занятие за {DateTime.ToString("dd.MM.yyyy")} по курсу {CourseName} было оценено на {Grade}. {(EarnedCoins == 0 ? "" : $"{Emoticons.Coins}Начислено {EarnedCoins} монет!")}",
                }
            ];
        }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = StudentUserId,
                    Title = "Оценка занятия",
                    Text = $"Занятие за {DateTime.ToString("dd.MM.yyyy")} по курсу {CourseName} было оценено на {Grade}. {(EarnedCoins == 0 ? "" : $"Начислено {EarnedCoins} монет!")}",
                }
            ];
        }
    }
}
