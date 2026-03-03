using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Abstractions
{
    public interface IMailNotification: IPushable
    {
        Dictionary<int, string> ToMailNotifications();
        string MailTitle { get; }
    }
}
