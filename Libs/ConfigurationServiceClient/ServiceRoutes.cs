using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SolutionConfiguration
{
    public class ServiceRoutes
    {
        [JsonPropertyName("PrimumCore")]
        public ServiceConfiguration PrimumCore { get; set; } = null!;

        [JsonPropertyName("PrimumWebAPI")]
        public ServiceConfiguration PrimumWebAPI { get; set; } = null!;

        [JsonPropertyName("BotCore")]
        public ServiceConfiguration BotCore { get; set; } = null!;

        [JsonPropertyName("PublisherService")]
        public ServiceConfiguration PublisherService { get; set; } = null!;

        [JsonPropertyName("SignService")]
        public ServiceConfiguration SignService { get; set; } = null!;

        [JsonPropertyName("GatewayURL")]
        public string GatewayURL { get; set; } = string.Empty;
    }
}
