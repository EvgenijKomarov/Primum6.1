using Microsoft.EntityFrameworkCore;
using PrimumCore.Controllers;
using PrimumCore.Models;
using PrimumCore.Services.Iterators;
using PrimumCore.Utilities;

namespace PrimumCore.Extentions
{
    public static class BuilderExtentions
    {
        public static WebApplicationBuilder AddDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentIterator>();
            builder.Services.AddScoped<TeacherIterator>();
            builder.Services.AddScoped<AdminIterator>();
            builder.Services.AddScoped<UserIterator>();
            builder.Services.AddScoped<PasswordHasher>();
            builder.Services.AddScoped<CommonIterator>();


            return builder;
        }

        public static WebApplicationBuilder AddProjectControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentController>();
            builder.Services.AddScoped<UserController>();
            builder.Services.AddScoped<TeacherController>();
            builder.Services.AddScoped<AdminController>();
            builder.Services.AddScoped<PublicController>();

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
