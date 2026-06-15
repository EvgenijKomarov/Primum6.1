using CommonNotificationServiceClient.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonNotificationServiceClient
{
    public class CommonNotificationClient
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public CommonNotificationClient(string commonNotificationServiceUrl, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(commonNotificationServiceUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Ставит уведомление просмотренным
        /// </summary>
        public async Task<bool> SetSeenNotificationAsync(string notificationId, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsync($"/set-seen/{notificationId}", null, ct);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Получить все уведомления пользователя
        /// </summary>
        public async Task<IEnumerable<CommonNotification>> GetUserNotificationsAsync(int userId, CancellationToken ct = default)
        {
            var requestUrl = $"/by-user/{userId}";
            var response = await _httpClient.GetAsync(requestUrl, ct);

            if ((await response.Content.ReadAsStringAsync(ct)) == "{}")
            {
                return new List<CommonNotification>();
            }

            var result = await response.Content.ReadFromJsonAsync<List<CommonNotification>>(_jsonOptions, ct);

            return result ?? new List<CommonNotification>();
        }

        /*
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
        }*/
    }
}
