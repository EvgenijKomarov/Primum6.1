using CoreConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PrimumWebAPI.Controllers;
using PrimumWebAPI.Services;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
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

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out var methodInfo)
                        ? methodInfo.Name
                        : null;
                });
                c.TagActionsBy(api =>
                {
                    var tags = api.CustomAttributes()
                                  .OfType<TagsAttribute>()
                                  .FirstOrDefault();

                    return tags?.Tags.ToArray() ?? new[]
                    {
                        api.ActionDescriptor.RouteValues["controller"]
                    };
                });
                var scheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                };
                c.AddSecurityDefinition("bearer", scheme);
                c.AddSecurityRequirement(d => new OpenApiSecurityRequirement
                  {
                    {
                      new OpenApiSecuritySchemeReference("bearer", d),
                      new List<string>()
                    }
                  });
            });

            return builder;
        }

        public static WebApplicationBuilder AddClients(this WebApplicationBuilder builder, string coreUrl)
        {
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

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

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
