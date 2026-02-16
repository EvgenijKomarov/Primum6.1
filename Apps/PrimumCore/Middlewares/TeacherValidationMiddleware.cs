using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Exceptions;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class TeacherValidationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/teacher"))
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
                    .Include(x => x.TeacherProfile)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    throw new RequestingUserNotFoundException(userId);
                }
                if (user.TeacherProfile is null)
                {
                    throw new ProfileNotExistException("Teacher", userId);
                }
                else if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user))
                {
                    throw new NotAvailableException("Teacher");
                }

                await next(context);
                return;
            }

            throw new ArgumentException("Invalid id");
        }
    }
}
