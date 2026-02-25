using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables.Events
{
    public record UserVerifiedChatEvent: IPushable
    {
        public required int UserId { get; set; }
        public required long ChatId { get; set; }
        public required string RealizationTag { get; set; }
    }
}
