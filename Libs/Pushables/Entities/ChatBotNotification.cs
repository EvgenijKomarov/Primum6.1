using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Entities
{
    public record ChatBotNotification
    {
        public required int UserId { get; set; }
        public required string Text { get; set; }
    }
}
