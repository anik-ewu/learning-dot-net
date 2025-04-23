using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.Load("UserManager.Application"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<UserManager.API.Middleware.ExceptionMiddleware>();
app.UseMiddleware<UserManager.API.Middleware.AdminAuthorizationMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
