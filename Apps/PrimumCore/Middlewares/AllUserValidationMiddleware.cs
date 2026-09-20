using CoreDBModel.Models;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;

namespace PrimumCore.Middlewares
{
    public class AllUserValidationMiddleware(RequestDelegate next)
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
                    .GetRequiredService<DatabaseIterator>();
                var user = await dbContext.Users(false).FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    throw new RequestingUserNotFoundException(userId);
                }

                await next(context);
                return;
            }

            throw new ArgumentException("Invalid id");
        }
    }
}
