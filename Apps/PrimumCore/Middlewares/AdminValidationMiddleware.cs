using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Exceptions;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class AdminValidationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/admin"))
            {
                await next(context);
                return;
            }

            if (context.Request.RouteValues.TryGetValue("userId", out var userIdObj) &&
                int.TryParse(userIdObj?.ToString(), out int userId))
            {
                var dbContext = context.RequestServices
                    .GetRequiredService<IPrimumContext>();
                var user = await dbContext.Set<User>()
                    .Include(x => x.AdminProfile)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    throw new RequestingUserNotFoundException(userId);
                }
                else if (user.AdminProfile is null)
                {
                    throw new ProfileNotExistException("Admin", userId);
                }

                await next(context);
                return;
            }

            throw new ArgumentException("Invalid id");
        }
    }
}
