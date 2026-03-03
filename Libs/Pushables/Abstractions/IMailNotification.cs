using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Abstractions
{
    public interface IMailNotification: IPushable
    {
        Dictionary<int, string> ToMailNotifications();
        string MailTitle { get; }
    }
}
