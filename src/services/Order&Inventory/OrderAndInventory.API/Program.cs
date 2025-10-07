using OrderAndInventory.BLL;
using OrderAndInventory.DAL;
using OrderAndInventory.DAL.Database;
using Shared.ErrorHandling;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services
    .ConfigureDataAccessLayer(builder.Configuration)
    .ConfigureBusinessLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.MigrateDatabaseAsync();
}

app.UseExceptionHandler();

app.MapControllers();

app.Run();