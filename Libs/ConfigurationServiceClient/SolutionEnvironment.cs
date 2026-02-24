using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SolutionConfiguration
{
    public class SolutionEnvironment
    {
        [JsonPropertyName("PrimumCoreURL")]
        public string PrimumCoreURL { get; set; } = string.Empty;

        [JsonPropertyName("PrimumWebURL")]
        public string PrimumWebURL { get; set; } = string.Empty;

        [JsonPropertyName("BotCoreURL")]
        public string BotCoreURL { get; set; } = string.Empty;

        [JsonPropertyName("PublisherURL")]
        public string PublisherURL { get; set; } = string.Empty;

        [JsonPropertyName("GatewayURL")]
        public string GatewayURL { get; set; } = string.Empty;

        [JsonPropertyName("CoreDatabaseConnection")]
        public string CoreDatabaseConnection { get; set; } = string.Empty;

        [JsonPropertyName("RabbitMQConnection")]
        public string RabbitMQConnection { get; set; } = string.Empty;
    }
}
