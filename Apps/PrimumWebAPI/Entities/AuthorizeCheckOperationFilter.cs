using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PrimumWebAPI.Entities
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.GetCustomAttributes(true)
                .Concat(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor
                    ? descriptor.ControllerTypeInfo.GetCustomAttributes(true)
                    : Enumerable.Empty<Attribute>());

            // Если есть [AllowAnonymous] — убираем требования безопасности
            if (attributes.OfType<AllowAnonymousAttribute>().Any())
            {
                operation.Security = new List<OpenApiSecurityRequirement>();
                return;
            }

            // Если есть [Authorize] — добавляем требование
            if (attributes.OfType<AuthorizeAttribute>().Any())
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                          new OpenApiSecuritySchemeReference("bearer", context.Document),
                          new List<string>()
                        }
                    }
                };
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
            }
        }
    }
}
