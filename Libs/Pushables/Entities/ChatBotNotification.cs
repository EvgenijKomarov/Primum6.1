using SignServiceConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Entities
{
    public record ChatBotNotification
    {
        public required ChatSign ChatSign { get; set; }
        public required string Text { get; set; }
    }
}
