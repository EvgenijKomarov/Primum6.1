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
    public class LessonPreparationNotification : IChatBotNotification, IMailNotification
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

        public async Task<IEnumerable<ChatBotNotification>> GetChatBotNotifications(ChatBotSignInjector injector)
        {
            return ((await injector.InjectSign(StudentUserId)).Select(x => new ChatBotNotification
            {
                ChatSign = x,
                Text = $"{Emoticons.Time}Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName}.\n" +
                    (IsEnoughMoney ? $"{BoolRes._true}Вам должно хватить средств для оплаты занятия" : $"{BoolRes._false}Внимание! У вас недостаточно средств для оплаты занятия. Пожалуйста, пополните балланс."),
            }))
            .Concat((await injector.InjectSign(TeacherUserId)).Select(x => new ChatBotNotification
            {
                ChatSign = x,
                Text = $"{Emoticons.Time}Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName} с учеником {StudentName}",
            }));
        }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = TeacherUserId,
                    Title = "Уведомление о будущем занятии",
                    Text = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName} с учеником {StudentName}",
                },
                new MailNotification
                {
                    UserId = StudentUserId,
                    Title = "Уведомление о будущем занятии",
                    Text = $"Завтра случится занятие в {DateTime.ToString("HH:mm")} по курсу {CourseName}.\n" +
                    (IsEnoughMoney ? $"{BoolRes._true}Вам должно хватить средств для оплаты занятия" : $"{BoolRes._false}Внимание! У вас недостаточно средств для оплаты занятия. Пожалуйста, пополните балланс."),
                }
            ];
        }
    }
}
