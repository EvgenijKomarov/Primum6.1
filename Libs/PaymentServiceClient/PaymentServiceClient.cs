using PaymentServiceConnection.Exceptions;
using PaymentServiceConnection.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace PaymentServiceConnection
{
    public class PaymentServiceClient
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            Converters = { new DecimalJsonConverter() }
        };

        public PaymentServiceClient(HttpClient httpClient, string paymentServiceUrl)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = new Uri(paymentServiceUrl);
        }

        /// <summary>
        /// Принудительное пополнение баланса студента (минуя платежную систему)
        /// </summary>
        public async Task<PaymentResponse> ForceTopupStudentBalanceAsync(
            int userId,
            decimal amount,
            CancellationToken ct = default)
        {
            var request = new BalanceRequest(userId, amount);
            return await PostAsync("/force/topup-student-balance", request, ct);
        }

        /// <summary>
        /// Запрос на пополнение баланса через платежную систему
        /// </summary>
        public async Task<PaymentResponse> RequestTopupStudentBalanceAsync(
            int userId,
            decimal amount,
            CancellationToken ct = default)
        {
            var request = new BalanceRequest(userId, amount);
            return await PostAsync("/request-topup-student-balance", request, ct);
        }

        /// <summary>
        /// Запрос возврата средств с баланса студента
        /// </summary>
        public async Task<PaymentResponse> WithdrawStudentBalanceAsync(
            int userId,
            decimal amount,
            CancellationToken ct = default)
        {
            var request = new BalanceRequest(userId, amount);
            return await PostAsync("/withdrawn-student-balance", request, ct);
        }

        /// <summary>
        /// Обработка оплаты урока
        /// </summary>
        public async Task<PaymentResponse> ProcessLessonPaymentAsync(
            int studentUserId,
            int teacherUserId,
            decimal teacherCash,
            decimal platformCash,
            CancellationToken ct = default)
        {
            var request = new LessonPaymentRequest(studentUserId, teacherUserId, teacherCash, platformCash);
            return await PostAsync("/process-lesson-payment", request, ct);
        }

        private async Task<PaymentResponse> PostAsync(string endpoint, object payload, CancellationToken ct)
        {
            PaymentResponse? result;
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, payload, _jsonOptions, ct);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadFromJsonAsync<PaymentResponse>(_jsonOptions, ct);
                if (result == null) throw new InvalidOperationException("Empty response from server");
            }
            catch (HttpRequestException ex)
            {
                throw new PaymentServiceException($"HTTP request error: {ex.Message}", ex);
            }
            catch (TaskCanceledException)
            {
                throw new PaymentServiceException($"Request timeout");
            }
            catch (JsonException ex)
            {
                throw new PaymentServiceException($"JSON parsing error: {ex.Message}", ex);
            }
            if (result.Success == false)
            {
                throw new PaymentServiceException($"Payment service error: {result.Error}");
            }
            return result;
        }
    }
}
