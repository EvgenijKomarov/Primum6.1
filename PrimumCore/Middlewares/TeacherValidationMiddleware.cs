using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
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
                    .FirstOrDefaultAsync(x => x.Id == userId && x.TeacherProfile != null);

                if (user is null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Teacher not found",
                        userId
                    });
                    return;
                }
                else if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user))
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Teacher not approved",
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
