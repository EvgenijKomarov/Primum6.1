using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PaymentServiceConnection.Models
{
    public record PaymentResponse(
        [property: JsonPropertyName("success")] bool Success,
        [property: JsonPropertyName("error")] string? Error = null,
        [property: JsonPropertyName("url")] string? Url = null
    );
}
