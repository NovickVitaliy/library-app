using BookCatalog.Application;
using BookCatalog.Infrastructure;
using BookCatalog.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApplication()
    .ConfigureInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.SetupDatabase();
}

app.MapGet("/", () => "Hello World!");

app.Run();