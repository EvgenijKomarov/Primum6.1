using Pushables.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pushables.Events
{
    public class DeleteAbonementSheduleEvent : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int AbonementSheduleId { get; set; }

        public required string DayOfWeek { get; set; }

        public required int Time { get; set; }

        public string MailTitle => "Удаление абонемента одного из учеников";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Student}Ученик {StudentName} удалил расписание по курсу {CourseName} на {DayOfWeekRes.ResourceManager.GetString(DayOfWeek)} {Time}:00",
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Ученик {StudentName} удалил расписание по курсу {CourseName} на {DayOfWeekRes.ResourceManager.GetString(DayOfWeek)} {Time}:00",
            };
        }
    }
}
