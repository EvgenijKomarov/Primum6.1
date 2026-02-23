using BotCore.Exceptions;
using CoreConnection;
using Engine.Exceptions;

namespace BotCore.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API exception occurred");
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Response
                });
            }
            catch (ProcessFailureException ex)
            {
                _logger.LogError(ex, "Process exception occures");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
                });
            }
            catch (EndpointNodeNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
                });
            }
            catch (NodeNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
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
    }
}
