using Microsoft.EntityFrameworkCore;
using PrimumCore.Controllers;
using PrimumCore.Models;

namespace PrimumCore.Extentions
{
    public static class BuilderExtentions
    {
        public static WebApplicationBuilder AddDI(this WebApplicationBuilder builder)
        {

            return builder;
        }

        public static WebApplicationBuilder AddProjectControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<StudentController>();
            builder.Services.AddScoped<UserController>();


            return builder;
        }

        public static WebApplicationBuilder AddPrimumContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PrimumContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddScoped<IPrimumContext>(provider =>
                provider.GetRequiredService<PrimumContext>());

            return builder;
        }
    }
}
