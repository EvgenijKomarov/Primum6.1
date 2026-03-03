using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Abstractions
{
    public interface IChatBotNotification: IPushable
    {
        Dictionary<int, string> ToChatBotNotifications();
    }
}
