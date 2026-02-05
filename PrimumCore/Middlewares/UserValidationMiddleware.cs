using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class UserValidationMiddleware(RequestDelegate next, IPrimumContext dbContext)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.RouteValues.TryGetValue("userId", out var userIdObj) &&
                userIdObj is int userId)
            {
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
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Missing id"
            });
        }
    }
}
