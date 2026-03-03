using Pushables.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Timers;

namespace Pushables.Events
{
    public class UserEmailVerificationEvent: IMailNotification
    {
        public required string EmailAdress { get; set; }
        public required string VerificationLink { get; set; }
        public required int UserId { get; set; }

        public string MailTitle => "Подтверждение почты";
        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [UserId] = $"Для полного доступа на площадку, пожалуйста, перейдите по ссылке для подтверждения почты: {VerificationLink}",
            };
        }
    }
}
