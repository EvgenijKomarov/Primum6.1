using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSigns
{
    public class ChatSign
    {
        public string RealizationTag { get; set; } = null!;
        public long ChatId { get; set; }
        public string? UserName { get; set; }
    }
}
