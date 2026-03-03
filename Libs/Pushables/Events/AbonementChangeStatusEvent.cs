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
    public class AbonementChangeStatusEvent : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required string AbonementStatus { get; set; }

        public string MailTitle => "Изменение статуса абонемента";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Abonement}Абонемент (Id: {AbonementId}) по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Абонемент (Id: {AbonementId}) по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
            };
        }
    }
}
