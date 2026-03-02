using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SignServiceConnection.Models
{
    public class ChatSign
    {
        [JsonPropertyName("realizationTag")]
        public string RealizationTag { get; set; } = null!;

        [JsonPropertyName("chatId")]
        public long ChatId { get; set; }

        [JsonPropertyName("username")]
        public string? UserName { get; set; }
    }
}
