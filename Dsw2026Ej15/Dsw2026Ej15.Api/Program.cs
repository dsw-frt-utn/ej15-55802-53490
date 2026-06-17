using Dsw2026Ej15.Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Dsw2026Ej15.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapGet("/health-check", () => Results.Ok(new { status = "OK" }));

app.Run();