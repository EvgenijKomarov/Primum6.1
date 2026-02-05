using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class UserValidationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/user"))
            {
                await next(context);
                return;
            }

            if (context.Request.RouteValues.TryGetValue("userId", out var userIdObj) &&
                int.TryParse(userIdObj?.ToString(), out int userId))
            {
                var dbContext = context.RequestServices
                    .GetRequiredService<IPrimumContext>();
                var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "User not found",
                        userId
                    });
                    return;
                }
                else if (!AvailabilityExpressions.IsUserAvailable.Compile()(user))
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "User not available",
                        userId
                    });
                    return;
                }

                await next(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Missing id"
            });
        }
    }
}
