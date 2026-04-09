using BotCore.Extensions;
using BotCore.Middlewares;
using Microsoft.OpenApi;
using SolutionConfiguration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var solutionEnvironment = await new ConfigurationClient().GetRoutesAsync();
builder.Services.AddSingleton<ServiceRoutes>(sp => solutionEnvironment);
builder.WebHost.UseUrls(solutionEnvironment.BotCore.SelfUrl);
builder.AddSignService(solutionEnvironment.SignService.PublicUrl);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
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
builder.AddClients(solutionEnvironment.PrimumCore.PublicUrl);
builder.AddBotEngine();
builder.AddNodes();
builder.AddLogging();
builder.AddServices();
builder.AddMiddlewares();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Configuration.GetValue<bool>("SwaggerOn") == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
