using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PrimumCore.Middlewares;
using PrimumWebAPI.Controllers;
using PrimumWebAPI.Extensions;
using PrimumWebAPI.Services;
using SolutionConfiguration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var solutionEnvironment = await new ConfigurationClient().GetRoutesAsync();
builder.WebHost.UseUrls(solutionEnvironment.PrimumWebAPI.SelfUrl);

builder.Services.AddControllers();

builder.AddAuth();
builder.AddLogging();
builder.AddControllers();
builder.AddServices();
builder.AddClients(solutionEnvironment.PrimumCore.PublicUrl, solutionEnvironment.PaymentService.PublicUrl);
builder.AddSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
