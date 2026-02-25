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
        public required string VerificationHash { get; set; }
        public required int UserId { get; set; }

        public IEnumerable<MailNotification> GetMailNotifications()
        {
            return [
                new MailNotification
                {
                    UserId = UserId,
                    Text = $"Для полного доступа на площадку, пожалуйста, используйте этот код на сайте для подтверждения почты: {VerificationHash}",
                    EmailAdress = this.EmailAdress,
                }
            ];
        }
    }
}
