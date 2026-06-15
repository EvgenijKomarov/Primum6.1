using PublishServiceConnection.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublishServiceConnection.Events
{
    public class LessonPreparationEvent : IChatBotNotification, IMailNotification, ICommonNotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int LessonId { get; set; }

        public required DateTime DateTime { get; set; }

        public required bool IsEnoughMoney { get; set; }

        public string MailTitle => "Уведомление о будущем занятии";
        public Dictionary<int, string> ToChatBotNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"{Emoticons.Time}Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName} с учеником {StudentName}",
                [StudentUserId] = $"{Emoticons.Time}Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName}.\n" +
                    (IsEnoughMoney ? $"{BoolRes._true}Вам должно хватить средств для оплаты занятия" : $"{BoolRes._false}Внимание! У вас недостаточно средств для оплаты занятия. Пожалуйста, пополните балланс.")
            };
        }

        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName} с учеником {StudentName}",
                [StudentUserId] = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName}.\n" +
                    (IsEnoughMoney ? $"{BoolRes._true}Вам должно хватить средств для оплаты занятия" : $"{BoolRes._false}Внимание! У вас недостаточно средств для оплаты занятия. Пожалуйста, пополните балланс.")
            };
        }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName} с учеником {StudentName}",
                [StudentUserId] = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName}.\n" +
                    (IsEnoughMoney ? $"{BoolRes._true}Вам должно хватить средств для оплаты занятия" : $"{BoolRes._false}Внимание! У вас недостаточно средств для оплаты занятия. Пожалуйста, пополните балланс.")
            };
        }
    }
}
