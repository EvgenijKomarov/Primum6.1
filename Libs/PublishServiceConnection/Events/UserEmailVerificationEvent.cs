using PublishServiceConnection.Abstractions;
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
        public required int UserId { get; set; }

        public string MailTitle => "Подтверждение почты";
        public Dictionary<int, string> ToMailNotifications()
        {
            return new Dictionary<int, string>
            {
                [UserId] = $"Для полного доступа на площадку, пожалуйста, введите токен подтверждения на сайте в личном кабинете: {Token}",
            };
        }
    }
}
