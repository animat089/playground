using System.Text.Json;
using AnimatLabs.OutboxPattern.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapPost("/api/orders", async (OrderRequest req, AppDbContext db) =>
{
    await using var tx = await db.Database.BeginTransactionAsync();

    var order = new Order
    {
        Customer = req.Customer,
        Product = req.Product,
        Quantity = req.Quantity,
        TotalAmount = req.TotalAmount
    };

    db.Orders.Add(order);
    await db.SaveChangesAsync();

    var outboxEvent = new OutboxEvent
    {
        Id = Guid.NewGuid(),
        AggregateType = "Order",
        AggregateId = order.Id.ToString(),
        EventType = "OrderCreated",
        Payload = JsonSerializer.Serialize(new
        {
            order.Id,
            order.Customer,
            order.Product,
            order.Quantity,
            order.TotalAmount,
            order.Status
        })
    };

    db.Outbox.Add(outboxEvent);
    await db.SaveChangesAsync();
    await tx.CommitAsync();

    return Results.Ok(new { order.Id, order.Status, OutboxEventId = outboxEvent.Id });
});

app.MapGet("/api/outbox", async (AppDbContext db) =>
{
    var events = await db.Outbox.OrderByDescending(e => e.CreatedAt).Take(10).ToListAsync();
    return Results.Ok(events);
});

app.Run();

record OrderRequest(string Customer, string Product, int Quantity, decimal TotalAmount);
