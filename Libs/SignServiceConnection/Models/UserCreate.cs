using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SignServiceConnection.Models
{
    public class UserCreate
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("realizationTag")]
        public string RealizationTag { get; set; } = string.Empty;

        [JsonPropertyName("chatId")]
        public long ChatId { get; set; }  // long, т.к. chatId в Telegram может быть > int.MaxValue

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
    }
}
