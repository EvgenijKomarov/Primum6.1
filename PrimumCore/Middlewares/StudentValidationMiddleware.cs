using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class StudentValidationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/student"))
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
                    .Include(x => x.StudentProfile)
                    .FirstOrDefaultAsync(x => x.Id == userId && x.StudentProfile != null);

                if (user is null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Student not found",
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
