using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PaymentServiceConnection.Models
{
    public record BalanceRequest(
        [property: JsonPropertyName("userId")] int UserId,
        [property: JsonPropertyName("amount")] decimal Amount
    );
}
