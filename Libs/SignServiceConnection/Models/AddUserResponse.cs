using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SignServiceConnection.Models
{
    // 🔹 Ответ: POST /add
    public class AddUserResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
