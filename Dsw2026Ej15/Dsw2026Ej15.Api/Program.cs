using Dsw2026Ej15.Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Dsw2026Ej15.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/health-check", () => Results.Ok(new { status = "OK" }));

app.Run();