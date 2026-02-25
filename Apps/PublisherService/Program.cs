using Publisher.Extensions;
using SolutionConfiguration;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var solutionEnvironment = await new ConfigurationClient().GetConfigurationAsync();
builder.WebHost.UseUrls(solutionEnvironment.PublisherService.SelfUrl);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddPublisher(solutionEnvironment.RabbitMQConnection);
builder.AddLogging();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
