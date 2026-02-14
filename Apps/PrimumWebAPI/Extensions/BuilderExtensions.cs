using CoreConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PrimumWebAPI.Controllers;
using PrimumWebAPI.Services;
using System.Text;

namespace PrimumWebAPI.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UserController>();
            builder.Services.AddScoped<StudentController>();
            builder.Services.AddScoped<AdminController>();
            builder.Services.AddScoped<PublicController>();
            builder.Services.AddScoped<TeacherController>();
            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<JwtTokenService>();
            return builder;
        }

        public static WebApplicationBuilder AddClients(this WebApplicationBuilder builder)
        {
            string coreUrl = builder.Configuration["Api:CoreURL"] ?? "https://localhost:5001";

            builder.Services.AddHttpClient<AdminClient>()
                .AddTypedClient((httpClient, sp) => new AdminClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<StudentClient>()
                .AddTypedClient((httpClient, sp) => new StudentClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<UserClient>()
                .AddTypedClient((httpClient, sp) => new UserClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<PublicClient>()
                .AddTypedClient((httpClient, sp) => new PublicClient(coreUrl, httpClient));
            builder.Services.AddHttpClient<TeacherClient>()
                .AddTypedClient((httpClient, sp) => new TeacherClient(coreUrl, httpClient));

            return builder;
        }

        public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
            return builder;
        }
    }
}
