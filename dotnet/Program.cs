using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.App.DbContext;
using TodoApi.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var dbConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ??
                         "Host=localhost;Database=todo_api;Username=todo_api_rw;Password=hello_rust";
builder.Services.AddPooledDbContextFactory<TodoApiContext>(
    options => options.UseNpgsql(dbConnectionString).EnableServiceProviderCaching(false), 32);
builder.Services.AddScoped(ctx => ctx.GetRequiredService<IDbContextFactory<TodoApiContext>>().CreateDbContext());
builder.Services.AddScoped<ITodoService, TodoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todo", ([FromServices] ITodoService todoService) => Results.Ok(todoService.GetAll().ToList()));
app.Run();