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
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("CONFIG_SERVICE_URL") ?? "http://localhost:5000");
        }

        public async Task<SolutionEnvironment> GetConfigurationAsync(CancellationToken cancellationToken = default)
        {
            SolutionEnvironment config = null;
            while(config == null)
            {
                try
                {
                    var response = await _httpClient.GetAsync("/config", cancellationToken);
                    response.EnsureSuccessStatusCode();

                    config = await response.Content.ReadFromJsonAsync<SolutionEnvironment>(_jsonOptions, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch configuration: {ex.Message}. Retrying in 5 seconds...");
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }

            return config ?? throw new InvalidOperationException("Failed to parse configuration: received null");
        }
    }
}
