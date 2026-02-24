using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolutionConfiguration
{
    public class ConfigurationClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public ConfigurationClient() : this(new HttpClient())
        {
        }
        internal ConfigurationClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<SolutionEnvironment> GetConfigurationAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("/config", cancellationToken);
            response.EnsureSuccessStatusCode();

            var config = await response.Content.ReadFromJsonAsync<SolutionEnvironment>(_jsonOptions, cancellationToken);

            return config ?? throw new InvalidOperationException("Failed to parse configuration: received null");
        }
    }
}
