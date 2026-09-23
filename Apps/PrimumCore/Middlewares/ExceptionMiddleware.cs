using PrimumCore.Exceptions;

namespace PrimumCore.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ProfileNotExistException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (RequestingUserNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (InvalidCredentialsException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                // Нарушение бизнес-правила — ошибка запроса, а не сбой сервера
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NoPermissionException ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotAvailableException ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.StatusCode = 520;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
