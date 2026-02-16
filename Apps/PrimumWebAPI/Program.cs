using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PrimumCore.Middlewares;
using PrimumWebAPI.Controllers;
using PrimumWebAPI.Extensions;
using PrimumWebAPI.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.AddAuth();
builder.AddControllers();
builder.AddServices();
builder.AddClients();
builder.AddSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Configuration.GetValue<bool>("SwaggerOn") == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
