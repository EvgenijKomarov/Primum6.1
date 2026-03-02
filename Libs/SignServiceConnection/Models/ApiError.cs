using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SignServiceConnection.Models
{
    public class ApiError
    {
        [JsonPropertyName("detail")]
        public string Detail { get; set; } = string.Empty;
    }
}
