using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PublishServiceConnection.Events
{
    public class DeleteAbonementSheduleEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required TimeSpan TeacherTimezoneOffset { get; set; }

        public required string CourseName { get; set; }

        public required string TeacherEmail { get; set; }

        public required int AbonementId { get; set; }

        public required int AbonementSheduleId { get; set; }

        public required DayOfWeek DayOfWeek { get; set; }

        public required int Time { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            var date = ApplyOffset(DayOfWeek, Time, TeacherTimezoneOffset);

            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Student}Ученик {StudentName} удалил расписание по курсу {CourseName} на {DayOfWeekRes.ResourceManager.GetString(date.Day.ToString())} {date.Hour}:00",
            };
        }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            var date = ApplyOffset(DayOfWeek, Time, TeacherTimezoneOffset);

            list.Add(new Email
            {
                Address = TeacherEmail,
                MailTitle = "Удаление абонемента одного из учеников",
                Data = new()
                {
                    ["body"] = $"Ученик {StudentName} удалил расписание по курсу {CourseName} на {DayOfWeekRes.ResourceManager.GetString(date.Day.ToString())} {date.Hour}:00"
                },
                EmailTemplate = EmailTemplate.InfoEmail,
                Priority = EmailPriority.NORMAL
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            var date = ApplyOffset(DayOfWeek, Time, TeacherTimezoneOffset);

            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Ученик {StudentName} удалил расписание по курсу {CourseName} на {DayOfWeekRes.ResourceManager.GetString(date.Day.ToString())} {date.Hour}:00",
            };
        }

        private (DayOfWeek Day, int Hour) ApplyOffset(DayOfWeek day, int hour, TimeSpan offset)
        {
            const int minutesInDay = 24 * 60;
            const int minutesInWeek = 7 * minutesInDay;

            // Переводим исходные день+час в общее число минут с начала недели
            int totalMinutes = (int)day * minutesInDay + hour * 60;

            // Прибавляем смещение (учитываем и часы, и минуты из TimeSpan)
            totalMinutes += (int)offset.TotalMinutes;

            // Корректно оборачиваем по модулю недели (в C# % может давать отрицательный результат)
            totalMinutes %= minutesInWeek;
            if (totalMinutes < 0)
                totalMinutes += minutesInWeek;

            int resultDay = totalMinutes / minutesInDay;
            int resultHour = (totalMinutes % minutesInDay) / 60;

            return ((DayOfWeek)resultDay, resultHour);
        }
    }
}
