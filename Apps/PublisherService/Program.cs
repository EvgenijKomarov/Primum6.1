using CoreConnection;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Publisher.Extensions;
using SignServiceConnection;
using SolutionConfiguration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var solutionEnvironment = await new ConfigurationClient().GetConfigurationAsync();
builder.WebHost.UseUrls(solutionEnvironment.PublisherService.SelfUrl);
builder.Services.AddHttpClient<SignServiceClient>()
    .AddTypedClient((httpclient, sp) => new SignServiceClient(solutionEnvironment.SignService.PublicUrl, httpclient));
builder.Services.AddHttpClient<UserClient>()
    .AddTypedClient((httpclient, sp) => new UserClient(solutionEnvironment.PrimumCore.PublicUrl, httpclient));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Publisher Service",
        Version = "v1",
        Description = "Сервис пуша уведомлений"
    });
    // Подключение XML-комментариев
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    // ⚠️ Важно: второй параметр true включает комментарии для контроллеров!
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

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
