using Microsoft.AspNetCore.Mvc.Controllers;
using PrimumCore.Extentions;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
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
});

builder.AddDI();
builder.AddContext();
builder.AddProjectControllers();
builder.AddPeriodWorkers();
builder.AddPublishers();
builder.AddLogging();

var app = builder.Build();

if (app.Configuration.GetValue<bool>("SwaggerOn") == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.AddMiddlewares();
app.MapControllers();

app.Run();
