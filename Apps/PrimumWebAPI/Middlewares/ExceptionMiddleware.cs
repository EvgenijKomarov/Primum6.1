using CoreConnection;
using System.Text.Json;

namespace PrimumCore.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
    {
        public const string InvalidInputMessage = "Invalid input";

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ApiException ex)
            {
                _logger.LogError(ex, "Core exception occurred");
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ExtractCoreError(ex.Response)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.StatusCode = 520;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Unhandled exception occurred"
                });
            }
        }

        // Ядро отвечает либо текстом ошибки, либо ProblemDetails (ошибка валидации ASP.NET).
        // ProblemDetails не пробрасываем строкой: клиенту не нужны traceId и имена внутренних классов.
        private static string? ExtractCoreError(string? response)
        {
            if (string.IsNullOrWhiteSpace(response) || !response.TrimStart().StartsWith('{')) { return response; }

            try
            {
                using var json = JsonDocument.Parse(response);
                if (json.RootElement.TryGetProperty("error", out var error) && error.ValueKind == JsonValueKind.String)
                { return error.GetString(); }
                if (json.RootElement.TryGetProperty("title", out _))
                { return InvalidInputMessage; }
            }
            catch (JsonException) { }

            return response;
        }
    }
}
