using BotCore.Extensions;
using BotCore.Middlewares;
using SolutionConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var solutionEnvironment = await new ConfigurationClient().GetConfigurationAsync();
builder.Services.AddSingleton<SolutionEnvironment>(sp => solutionEnvironment);
builder.WebHost.UseUrls(solutionEnvironment.BotCore.SelfUrl);
builder.AddSignService(solutionEnvironment.SignService.PublicUrl);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
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

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
