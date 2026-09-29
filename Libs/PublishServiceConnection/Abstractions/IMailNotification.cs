using PublishServiceConnection.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Abstractions
{
    public class Email
    {
        public required Dictionary<string, string> Data { get; set; }

        public required string MailTitle { get; set; }

        public required EmailTemplate EmailTemplate { get; set; }

        public required string Address { get; set; }
    }
    public interface IMailNotification: IPushable
    {
        List<Email> ToMailNotifications();
    }
}
