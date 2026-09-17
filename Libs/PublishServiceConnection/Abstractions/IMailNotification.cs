using PublishServiceConnection.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Abstractions
{
    public interface IMailNotification: IPushable
    {
        Dictionary<string, string> ToMailNotifications();
        string MailTitle { get; }
        EmailTemplate Template { get; }
    }
}
