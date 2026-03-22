using AnimatLabs.MartenEventStore.Events;
using AnimatLabs.MartenEventStore.Projections;
using Marten;
using Marten.Events.Projections;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? "Host=localhost;Port=5434;Database=marten_demo;Username=postgres;Password=postgres";

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.Projections.Add<OrderSummaryProjection>(ProjectionLifecycle.Inline);
});

var app = builder.Build();

app.MapPost("/api/orders", async (PlaceOrderRequest req, IDocumentSession session) =>
{
    var orderId = Guid.NewGuid();
    var placed = new OrderPlaced(orderId, req.Customer, req.Product, req.Quantity, req.Total);
    session.Events.StartStream<OrderSummary>(orderId, placed);
    await session.SaveChangesAsync();
    return Results.Ok(new { orderId });
});

app.MapPost("/api/orders/{id}/confirm", async (Guid id, IDocumentSession session) =>
{
    session.Events.Append(id, new OrderConfirmed(id, DateTime.UtcNow));
    await session.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/api/orders/{id}/ship", async (Guid id, ShipRequest req, IDocumentSession session) =>
{
    session.Events.Append(id, new OrderShipped(id, req.TrackingNumber, DateTime.UtcNow));
    await session.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/api/orders/{id}/cancel", async (Guid id, CancelRequest req, IDocumentSession session) =>
{
    session.Events.Append(id, new OrderCancelled(id, req.Reason, DateTime.UtcNow));
    await session.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/api/orders/{id}", async (Guid id, IQuerySession session) =>
{
    var summary = await session.LoadAsync<OrderSummary>(id);
    return summary is null ? Results.NotFound() : Results.Ok(summary);
});

app.MapGet("/api/orders/{id}/events", async (Guid id, IQuerySession session) =>
{
    var events = await session.Events.FetchStreamAsync(id);
    return Results.Ok(events.Select(e => new { e.EventTypeName, e.Timestamp, Data = e.Data }));
});

app.MapGet("/api/orders", async (IQuerySession session) =>
{
    var orders = await session.Query<OrderSummary>().ToListAsync();
    return Results.Ok(orders);
});

app.Run();

record PlaceOrderRequest(string Customer, string Product, int Quantity, decimal Total);
record ShipRequest(string TrackingNumber);
record CancelRequest(string Reason);
