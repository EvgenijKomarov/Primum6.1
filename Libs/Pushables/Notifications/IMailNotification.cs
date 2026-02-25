using Pushables.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Notifications
{
    public interface IMailNotification : IPushable
    {
        IEnumerable<MailNotification> GetMailNotifications();
    }
}
