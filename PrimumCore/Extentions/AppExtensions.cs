using PrimumCore.Middlewares;

namespace PrimumCore.Extentions
{
    public static class AppExtensions
    {
        public static WebApplication AddMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TeacherValidationMiddleware>();
            app.UseMiddleware<StudentValidationMiddleware>();
            app.UseMiddleware<AdminValidationMiddleware>();
            app.UseMiddleware<AllUserValidationMiddleware>();

            return app;
        }
    }
}
