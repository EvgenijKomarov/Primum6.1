using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables.Entities
{
    public record MailNotification
    {
        public required int UserId { get; set; }
        public required string Text { get; set; }
        public string? EmailAdress { get; set; } = null;
    }
}
