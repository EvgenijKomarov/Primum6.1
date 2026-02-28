using Pushables.Entities;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Timers;

namespace Pushables.Notifications
{
    public class UserEmailVerificationNotification: IMailNotification
    {
        public required string EmailAdress { get; set; }
        public required string VerificationLink { get; set; }
        public required int UserId { get; set; }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = UserId,
                    Title = "Подтверждение почты",
                    Text = $"Для полного доступа на площадку, пожалуйста, перейдите по ссылке для подтверждения почты: {VerificationLink}",
                    EmailAdress = this.EmailAdress,
                }
            ];
        }
    }
}
