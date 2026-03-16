var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { service = "api", time = DateTime.UtcNow });

app.MapGet("/orders", () => new[]
{
    new { id = 1, product = "Widget", price = 29.99 },
    new { id = 2, product = "Gadget", price = 49.99 },
    new { id = 3, product = "Doohickey", price = 9.99 }
});

app.MapGet("/health", () => Results.Ok("healthy"));

app.Run();
