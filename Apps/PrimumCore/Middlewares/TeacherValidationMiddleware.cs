using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;

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
                    .GetRequiredService<DatabaseIterator>();
                var user = await dbContext.Users(true)
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
                else if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user.TeacherProfile))
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
