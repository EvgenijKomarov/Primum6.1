using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublishServiceConnection.Events
{
    public class AbonementChangeStatusEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string TeacherEmail { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required string AbonementStatus { get; set; }

        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Abonement}Абонемент по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
            };
        }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            list.Add(new Email
            {
                Address = TeacherEmail,
                MailTitle = "Изменение статуса абонемента",
                Data = new()
                {
                    ["body"] = $"Абонемент по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}"
                },
                EmailTemplate = EmailTemplate.InfoEmail
            });
            return list;
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Абонемент по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
            };
        }
    }
}
