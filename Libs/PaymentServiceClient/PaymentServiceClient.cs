using PaymentServiceConnection.Exceptions;
using PaymentServiceConnection.Models;
using System.Net;
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
        /// Запрос на пополнение баланса через платежную систему
        /// </summary>
        public async Task<PaymentResponse> RequestTopupStudentBalanceAsync(
            int userId,
            decimal amount,
            CancellationToken ct = default)
        {
            return await PostAsync(Inv($"/request-topup-student-balance?userId={userId}&amount={amount}"), ct);
        }

        /// <summary>
        /// Запрос возврата средств с баланса студента
        /// </summary>
        public async Task<PaymentResponse> WithdrawStudentBalanceAsync(
            int userId,
            decimal amount,
            CancellationToken ct = default)
        {
            return await PostAsync(Inv($"/withdrawn-student-balance?userId={userId}&amount={amount}"), ct);
        }

        /// <summary>
        /// Обработка оплаты урока
        /// </summary>
        public async Task<PaymentResponse> ProcessLessonPaymentAsync(
            int lessonId,
            int studentUserId,
            int teacherUserId,
            decimal teacherCash,
            decimal platformCash,
            CancellationToken ct = default)
        {
            return await PostAsync(Inv($"/process-lesson-payment?lessonId={lessonId}&studentUserId={studentUserId}&teacherUserId={teacherUserId}&teacherCash={teacherCash}&platformCash={platformCash}"), ct);
        }

        public async Task<int?> GetStudentBalanceAsync(int studentUserId, CancellationToken ct = default)
        {
            int? amount = null;
            try
            {
                var response = await _httpClient.GetAsync($"/get-student-balance/{studentUserId}");
                response.EnsureSuccessStatusCode();
                amount = int.Parse(await response.Content.ReadAsStringAsync(ct));
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
            catch (Exception ex) 
            {
                throw new PaymentServiceException($"Unknown exception: {ex.Message}", ex);
            }
            return amount;
        }

        public async Task<bool> IsTeacherReadyAsync(int teacherUserId, CancellationToken ct = default)
        {
            bool amount = false;
            try
            {
                var response = await _httpClient.GetAsync($"/is-teacher-ready/{teacherUserId}");
                response.EnsureSuccessStatusCode();
                amount = bool.Parse(await response.Content.ReadAsStringAsync(ct));
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
            catch (Exception ex)
            {
                throw new PaymentServiceException($"Unknown exception: {ex.Message}", ex);
            }
            return amount;
        }

        private async Task<PaymentResponse> PostAsync(string endpoint, CancellationToken ct)
        {
            PaymentResponse? result;
            try
            {
                var response = await _httpClient.PostAsync(endpoint, null);
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
                throw new EquiringException($"Equiring service error: {result.Error}");
            }
            return result;
        }

        private static string Inv(FormattableString s) => FormattableString.Invariant(s);
    }
}
