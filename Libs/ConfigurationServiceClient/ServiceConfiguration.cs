using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SolutionConfiguration
{
    public class ServiceConfiguration
    {
        [JsonPropertyName("SelfUrl")]
        public string SelfUrl { get; set; } = string.Empty;

        [JsonPropertyName("PublicUrl")]
        public string PublicUrl { get; set; } = string.Empty;
    }
}
