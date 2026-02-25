using Pushables;
using Pushables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushables.Notifications
{
    public interface IChatBotNotification: IPushable
    {
        IEnumerable<ChatBotNotification> GetChatBotNotifications();
    }
}
