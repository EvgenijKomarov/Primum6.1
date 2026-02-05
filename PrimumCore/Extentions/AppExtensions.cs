using PrimumCore.Middlewares;

namespace PrimumCore.Extentions
{
    public static class AppExtensions
    {
        public static WebApplication AddMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<TeacherValidationMiddleware>();
            app.UseMiddleware<StudentValidationMiddleware>();
            app.UseMiddleware<AdminValidationMiddleware>();

            return app;
        }
    }
}
