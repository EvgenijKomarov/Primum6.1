using System;
using System.Collections.Generic;
using System.Text;

namespace PublishServiceConnection.Abstractions
{
    public interface ICommonNotification: IPushable
    {
        Dictionary<int, string> ToCommonNotifications();
    }
}
