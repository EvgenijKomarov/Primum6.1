using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SignServiceConnection.Models
{
    // 🔹 Ответ: GET /get-userId
    public class UserIdResponse
    {
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }
    }
}
