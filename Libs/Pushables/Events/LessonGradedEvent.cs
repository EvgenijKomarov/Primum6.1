using Pushables.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Events
{
    public class LessonGradedEvent : IChatBotNotification, IMailNotification
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

        public string MailTitle => "Оценка занятия";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [StudentUserId] = $"{Emoticons.Lesson}Занятие за {DateTime.ToString("dd.MM.yyyy")} по курсу {CourseName} было оценено на {Grade}. {(EarnedCoins == 0 ? "" : $"{Emoticons.Coins}Начислено {EarnedCoins} монет!")}",
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [StudentUserId] = $"Занятие за {DateTime.ToString("dd.MM.yyyy")} по курсу {CourseName} было оценено на {Grade}. {(EarnedCoins == 0 ? "" : $"Начислено {EarnedCoins} монет!")}",
            };
        }
    }
}
