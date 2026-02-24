using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;

namespace PrimumCore.Middlewares
{
    public class UserValidationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/available-user"))
            {
                await next(context);
                return;
            }

            if (context.Request.RouteValues.TryGetValue("userId", out var userIdObj) &&
                int.TryParse(userIdObj?.ToString(), out int userId))
            {
                var dbContext = context.RequestServices
                    .GetRequiredService<PrimumContext>();
                var user = await dbContext.Set<User>().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    throw new RequestingUserNotFoundException(userId);
                }
                else if (!AvailabilityExpressions.IsUserAvailable.Compile()(user))
                {
                    throw new NotAvailableException("User");
                }

                await next(context);
                return;
            }

            throw new ArgumentException("Invalid id");
        }
    }
}
