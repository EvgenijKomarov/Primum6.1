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
    public class AbonementChangeStatusNotification : IChatBotNotification, IMailNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required string AbonementStatus { get; set; }

        public IEnumerable<ChatBotNotification> GetChatBotNotifications()
        {
            return [
                new ChatBotNotification 
                {
                    UserId = TeacherUserId,
                    Text = $"{Emoticons.Abonement}Абонемент (Id: {AbonementId}) по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
                }
            ];
        }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = TeacherUserId,
                    Title = "Изменение статуса абонемента одного из учеников",
                    Text = $"Абонемент (Id: {AbonementId}) по курсу {CourseName} ученика {StudentName} изменил статус на {AbonementStatusRes.ResourceManager.GetString(AbonementStatus)}",
                }
            ];
        }
    }
}
