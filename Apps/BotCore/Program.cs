using BotCore.Extensions;
using BotCore.Middlewares;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSignService();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bot Core",
        Version = "v1",
        Description = "Сервис обработки сообщений из чат ботов"
    });
    // Подключение XML-комментариев
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    // ⚠️ Важно: второй параметр true включает комментарии для контроллеров!
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
builder.AddClients();
builder.AddBotEngine();
builder.AddNodes();
builder.AddLogging();
builder.AddServices();
builder.AddMiddlewares();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
