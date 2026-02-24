using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;

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
                    .GetRequiredService<PrimumContext>();
                var user = await dbContext.Set<User>().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == userId);

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
