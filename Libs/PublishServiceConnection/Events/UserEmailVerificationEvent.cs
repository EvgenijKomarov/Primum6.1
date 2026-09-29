using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Timers;

namespace PublishServiceConnection.Events
{
    public class UserEmailVerificationEvent: IMailNotification
    {
        public required string EmailAdress { get; set; }

        public required string Token { get; set; }

        public required string AuthUrl { get; set; }

        public required int UserId { get; set; }

        public List<Email> ToMailNotifications()
        {
            List<Email> list = new List<Email>();
            var link = $"{AuthUrl.TrimEnd('/')}/confirm-email?token={Uri.EscapeDataString(Token)}";

            list.Add(new Email
            {
                Address = EmailAdress,
                MailTitle = "Подтверждение почты",
                Data = new()
                {
                    ["link"] = link,
                    ["token"] = Token,
                },
                EmailTemplate = EmailTemplate.ConfirmationEmail
            });
            return list;
        }
    }
}
