using Microsoft.EntityFrameworkCore;
using PrimumCore.Controllers;
using PrimumCore.Models;
using PrimumCore.Services;
using PrimumCore.Utilities;

namespace PrimumCore.Extentions
{
    public static class BuilderExtentions
    {
        public static WebApplicationBuilder AddDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentIterator>();
            builder.Services.AddScoped<TeacherIterator>();
            builder.Services.AddScoped<UserIterator>();
            builder.Services.AddScoped<PasswordHasher>();
            

            return builder;
        }

        public static WebApplicationBuilder AddProjectControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentController>();
            builder.Services.AddScoped<UserController>();
            builder.Services.AddScoped<TeacherController>();
            builder.Services.AddScoped<AdminController>();

            return builder;
        }

        public static WebApplicationBuilder AddPrimumContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PrimumContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPrimumContext>(provider =>
                provider.GetRequiredService<PrimumContext>());

            return builder;
        }
    }
}
