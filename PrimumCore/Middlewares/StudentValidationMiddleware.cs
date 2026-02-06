using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
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
                    throw new NotAuthorizedException("Student", userId);
                }

                await next(context);
                return;
            }

            throw new NotAuthorizedException("Student");
        }
    }
}
