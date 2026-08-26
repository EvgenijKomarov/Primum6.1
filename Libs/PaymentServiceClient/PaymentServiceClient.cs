using PaymentServiceConnection.Exceptions;
using PaymentServiceConnection.Models;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace PaymentServiceConnection
{
    public class PaymentServiceClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters = { new DecimalJsonConverter() }
        };

        public PaymentServiceClient(HttpClient httpClient)
        {
            var url = Environment.GetEnvironmentVariable("PAYMENTSERVICE_URL") ?? throw new ArgumentNullException("Missing env variable");

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Payment service URL must not be empty.", nameof(url));

            _httpClient.BaseAddress = new Uri(url);
        }

        /// <summary>
        /// Запрос на пополнение баланса через платежную систему
        /// </summary>
        public async Task<string> RequestTopupStudentBalanceAsync(int userId, decimal amount, CancellationToken ct = default)
        {
            var url = BuildUrl("/request-topup-student-balance",
                ("userId", userId.ToString(CultureInfo.InvariantCulture)),
                ("amount", amount.ToString(CultureInfo.InvariantCulture)));

            return await PostForRawStringAsync(url, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Запрос возврата средств с баланса студента
        /// </summary>
        public async Task<bool> WithdrawStudentBalanceAsync(int userId, decimal amount, CancellationToken ct = default)
        {
            var url = BuildUrl("/withdrawn-student-balance",
                ("userId", userId.ToString(CultureInfo.InvariantCulture)),
                ("amount", amount.ToString(CultureInfo.InvariantCulture)));

            var raw = await PostForRawStringAsync(url, ct).ConfigureAwait(false);

            return ParseBool(raw);
        }

        /// <summary>
        /// Обработка оплаты урока
        /// </summary>
        public async Task<bool> ProcessLessonPaymentAsync(
            int lessonId,
            int studentUserId,
            int teacherUserId,
            decimal teacherCash,
            decimal platformCash,
            CancellationToken ct = default)
        {
            var url = BuildUrl("/process-lesson-payment",
                ("lessonId", lessonId.ToString(CultureInfo.InvariantCulture)),
                ("studentUserId", studentUserId.ToString(CultureInfo.InvariantCulture)),
                ("teacherUserId", teacherUserId.ToString(CultureInfo.InvariantCulture)),
                ("teacherCash", teacherCash.ToString(CultureInfo.InvariantCulture)),
                ("platformCash", platformCash.ToString(CultureInfo.InvariantCulture)));

            var raw = await PostForRawStringAsync(url, ct).ConfigureAwait(false);

            return ParseBool(raw);
        }

        /// <summary>
        /// Получение баланса студента
        /// </summary>
        public async Task<decimal?> GetStudentBalanceAsync(int studentUserId, CancellationToken ct = default)
        {
            var raw = await GetRawStringAsync($"/get-student-balance/{studentUserId}", ct).ConfigureAwait(false);

            if (!decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
                throw new PaymentServiceException($"Unexpected balance format: '{raw}'");

            return amount;
        }

        /// <summary>
        /// Проверка готовности преподавателя
        /// </summary>
        public async Task<bool> IsTeacherReadyAsync(int teacherUserId, CancellationToken ct = default)
        {
            var raw = await GetRawStringAsync($"/is-teacher-ready/{teacherUserId}", ct).ConfigureAwait(false);
            return ParseBool(raw);
        }

        /// <summary>
        /// Регистрация преподавателя в эквайринге
        /// </summary>
        public async Task<bool> EnroleTeacherRegistrationAsync(int teacherUserId, CancellationToken ct = default)
        {
            var raw = await PostForRawStringAsync($"/enrole-teacher-registration/{teacherUserId}", ct).ConfigureAwait(false);
            return ParseBool(raw);
        }

        /// <summary>
        /// Регистрация преподавателя как получателя выплат
        /// </summary>
        public async Task<bool> RegTeacherAsync(
            int teacherUserId,
            string fullName,
            string inn,
            string phone,
            string accountNumber,
            string bankBic,
            CancellationToken ct = default)
        {
            RequireNonEmpty(fullName, nameof(fullName));
            RequireNonEmpty(inn, nameof(inn));
            RequireNonEmpty(phone, nameof(phone));
            RequireNonEmpty(accountNumber, nameof(accountNumber));
            RequireNonEmpty(bankBic, nameof(bankBic));

            var url = BuildUrl("/register-teacher",
                ("teacherUserId", teacherUserId.ToString(CultureInfo.InvariantCulture)),
                ("fullName", fullName),
                ("inn", inn),
                ("phone", phone),
                ("accountNumber", accountNumber),
                ("bankBik", bankBic));

            var raw = await PostForRawStringAsync(url, ct).ConfigureAwait(false);
            return ParseBool(raw);
        }

        private async Task<string> GetRawStringAsync(string endpoint, CancellationToken ct)
        {
            var response = await SendAsync(() => _httpClient.GetAsync(endpoint, ct), ct).ConfigureAwait(false);
            return await ReadStringAsync(response, ct).ConfigureAwait(false);
        }

        private async Task<string> PostForRawStringAsync(string endpoint, CancellationToken ct)
        {
            var response = await SendAsync(() => _httpClient.PostAsync(endpoint, null, ct), ct).ConfigureAwait(false);
            return await ReadStringAsync(response, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Единая точка обработки транспортных ошибок для всех запросов.
        /// </summary>
        private static async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> requestFunc, CancellationToken ct)
        {
            try
            {
                var response = await requestFunc().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new PaymentServiceException($"HTTP request error: {ex.Message}", ex);
            }
            catch (TaskCanceledException) when (!ct.IsCancellationRequested)
            {
                // Distinguish an actual server-side timeout from a caller-initiated cancellation.
                throw new PaymentServiceException("Request timeout");
            }
        }

        private static async Task<string> ReadStringAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                return (await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false)).Trim();
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new PaymentServiceException($"Failed to read response body: {ex.Message}", ex);
            }
        }

        private static bool ParseBool(string raw)
        {
            if (!bool.TryParse(raw, out var value))
                throw new PaymentServiceException($"Unexpected boolean format: '{raw}'");

            return value;
        }

        private static void RequireNonEmpty(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{paramName}' must not be empty.", paramName);
        }

        private static string BuildUrl(string path, params (string Key, string Value)[] parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var (key, value) in parameters)
                query[key] = value;

            return $"{path}?{query}";
        }
    }
}