using PrimumCore.Entities;
using CoreDBModel.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using PrimumCore.Extentions;
using SolutionConfiguration;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var configClient = new ConfigurationClient();
var solutionEnvironment = await configClient.GetRoutesAsync();
var coreDbConnectionString = await configClient.GetCoreDatabaseConnectionAsync();

builder.WebHost.UseUrls(solutionEnvironment.PrimumCore.SelfUrl);
builder.Services.AddSingleton(sp => solutionEnvironment);

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
builder.AddContext(coreDbConnectionString);
builder.AddProjectControllers();
builder.AddPublishers(solutionEnvironment.PublisherService.PublicUrl);
builder.AddSignService(solutionEnvironment.SignService.PublicUrl);
builder.AddLogging();

var app = builder.Build();

if (app.Configuration.GetValue<bool>("SwaggerOn") == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.AddMiddlewares();
app.MapControllers();

app.Run();
