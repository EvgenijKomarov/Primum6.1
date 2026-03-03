using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Abstractions
{
    public interface IChatBotNotification: IPushable
    {
        Dictionary<int, string> ToChatBotNotifications();
    }
}
