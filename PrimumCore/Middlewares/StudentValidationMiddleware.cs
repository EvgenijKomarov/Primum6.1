using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Middlewares
{
    public class StudentValidationMiddleware(RequestDelegate next, IPrimumContext dbContext)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.RouteValues.TryGetValue("userId", out var userIdObj) &&
                userIdObj is int userId)
            {
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
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Missing id"
            });
        }
    }
}
