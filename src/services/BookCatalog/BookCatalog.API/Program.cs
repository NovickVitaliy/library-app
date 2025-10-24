using BookCatalog.Application;
using BookCatalog.Infrastructure;
using BookCatalog.Infrastructure.Database;
using Shared.ErrorHandling;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services
    .ConfigureApplication()
    .ConfigureInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SetupDatabase();
}

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();