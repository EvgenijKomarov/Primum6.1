using SignServiceConnection.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SignServiceConnection
{
    public class SignServiceClient
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public SignServiceClient(string url, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        public async Task<AddUserResponse> AddUserAsync(UserCreate user, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("/add", user, _jsonOptions, ct);
            await EnsureSuccessAsync(response, ct);
            return await response.Content.ReadFromJsonAsync<AddUserResponse>(_jsonOptions, ct)
                ?? throw new InvalidOperationException("Empty response from /add");
        }

        /// <summary>
        /// Получает userId по realizationTag и chatId
        /// </summary>
        public async Task<int> GetUserIdAsync(string realizationTag, long chatId, CancellationToken ct = default)
        {
            var requestUrl = $"/get-userId?realizationTag={Uri.EscapeDataString(realizationTag)}&chatId={chatId}";
            var response = await _httpClient.GetAsync(requestUrl, ct);
            await EnsureSuccessAsync(response, ct);

            var result = await response.Content.ReadFromJsonAsync<UserIdResponse>(_jsonOptions, ct)
                ?? throw new InvalidOperationException("Empty response from /get-userId");

            return result.UserId;
        }

        /// <summary>
        /// Получает словарь { realizationTag → username } для заданного userId
        /// </summary>
        public async Task<Dictionary<string, string>> GetUsernamesAsync(int userId, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"/get-usernames/{userId}", ct);
            await EnsureSuccessAsync(response, ct);

            return await response.Content.ReadFromJsonAsync<Dictionary<string, string>>(_jsonOptions, ct)
                ?? throw new InvalidOperationException("Empty response from /get-usernames");
        }

        /// <summary>
        /// Обрабатывает ошибки API (404, 409 и т.д.)
        /// </summary>
        private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken ct)
        {
            if (response.IsSuccessStatusCode) return;

            string errorDetail = string.Empty;
            try
            {
                var error = await response.Content.ReadFromJsonAsync<ApiError>(_jsonOptions, ct);
                errorDetail = error?.Detail ?? response.ReasonPhrase;
            }
            catch
            {
                errorDetail = await response.Content.ReadAsStringAsync(ct);
            }

            throw new SignServiceException(
                $"API Error {(int)response.StatusCode} ({response.StatusCode}): {errorDetail}",
                response.StatusCode)
            {
                RawContent = errorDetail
            };
        }
    }
}
