using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CommonNotificationServiceClient.Models
{
    public class CommonNotification
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = null!;

        [JsonPropertyName("seen")]
        public bool Seen { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime DateTime { get; set; }
    }
}
