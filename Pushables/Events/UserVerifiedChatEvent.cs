using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables.Events
{
    public class UserVerifiedChatEvent: IEvent
    {
        public int UserId { get; set; }
        public long ChatId { get; set; }
        public string RealizationTag { get; set; }
    }
}
