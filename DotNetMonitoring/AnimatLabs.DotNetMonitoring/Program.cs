using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, cfg) => cfg
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Console());

    builder.Services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live"])
        .AddNpgSql(
            builder.Configuration.GetConnectionString("Postgres")
                ?? "Host=localhost;Port=5432;Database=monitoring_demo;Username=postgres;Password=postgres",
            name: "postgresql",
            tags: ["ready"]);

    builder.Services.AddSingleton<OrderMetrics>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Prometheus metrics endpoint
    app.UseHttpMetrics();
    app.MapMetrics();

    // Liveness probe -- is the process alive?
    app.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("live"),
        ResponseWriter = WriteHealthResponse
    });

    // Readiness probe -- can it handle traffic? (checks DB connectivity)
    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = WriteHealthResponse
    });

    // Simulated order endpoint with custom metrics
    app.MapPost("/api/orders", async (OrderMetrics metrics) =>
    {
        var sw = Stopwatch.StartNew();

        var latency = Random.Shared.Next(50, 500);
        await Task.Delay(latency);

        var success = Random.Shared.Next(10) < 9;

        sw.Stop();
        metrics.RecordOrder(sw.Elapsed.TotalSeconds, success);

        return success
            ? Results.Ok(new { orderId = Guid.NewGuid(), status = "accepted" })
            : Results.Problem("Payment gateway timeout", statusCode: 503);
    });

    // Simple status page
    app.MapGet("/", () => Results.Content("""
        <h2>Monitoring Demo</h2>
        <ul>
            <li><a href="/health/live">Liveness</a></li>
            <li><a href="/health/ready">Readiness</a></li>
            <li><a href="/metrics">Prometheus Metrics</a></li>
        </ul>
        <p>POST /api/orders to generate order metrics.</p>
        """, "text/html"));

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

static Task WriteHealthResponse(HttpContext ctx, HealthReport report)
{
    ctx.Response.ContentType = "application/json";
    var result = new
    {
        status = report.Status.ToString(),
        checks = report.Entries.Select(e => new
        {
            name = e.Key,
            status = e.Value.Status.ToString(),
            duration = e.Value.Duration.TotalMilliseconds + "ms",
            description = e.Value.Description
        })
    };
    return ctx.Response.WriteAsJsonAsync(result);
}

public sealed class OrderMetrics
{
    private static readonly Histogram OrderDuration = Metrics.CreateHistogram(
        "app_order_duration_seconds",
        "Time to process an order",
        new HistogramConfiguration
        {
            Buckets = Histogram.ExponentialBuckets(0.05, 2, 8)
        });

    private static readonly Counter OrdersTotal = Metrics.CreateCounter(
        "app_orders_total",
        "Total orders processed",
        new CounterConfiguration
        {
            LabelNames = ["status"]
        });

    public void RecordOrder(double durationSeconds, bool success)
    {
        OrderDuration.Observe(durationSeconds);
        OrdersTotal.WithLabels(success ? "accepted" : "failed").Inc();
    }
}
