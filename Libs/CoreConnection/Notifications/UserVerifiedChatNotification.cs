using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.Notifications
{
    public class UserVerifiedChatNotification: INotification
    {
        public int UserId { get; set; }
        public long ChatId { get; set; }
        public string RealizationTag { get; set; }
    }
}
