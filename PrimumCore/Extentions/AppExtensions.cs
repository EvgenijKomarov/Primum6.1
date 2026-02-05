using PrimumCore.Middlewares;

namespace PrimumCore.Extentions
{
    public static class AppExtensions
    {
        public static WebApplication AddMiddlewares(this WebApplication app)
        {
            app.MapWhen(context =>
                context.Request.Path.StartsWithSegments("/api/teacher"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<TeacherValidationMiddleware>();
                });

            app.MapWhen(context =>
                context.Request.Path.StartsWithSegments("/api/student"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<StudentValidationMiddleware>();
                });

            app.MapWhen(context =>
                context.Request.Path.StartsWithSegments("/api/admin"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<AdminValidationMiddleware>();
                });

            return app;
        }
    }
}
