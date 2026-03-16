using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/events/clock", async (HttpContext ctx) =>
{
    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

    var ct = ctx.RequestAborted;

    await foreach (var tick in StreamClock(ct))
    {
        await ctx.Response.WriteAsync($"data: {tick:HH:mm:ss}\n\n", ct);
        await ctx.Response.Body.FlushAsync(ct);
    }
});

app.MapGet("/events/orders", async (HttpContext ctx) =>
{
    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

    var ct = ctx.RequestAborted;
    var orderNum = 1000;

    while (!ct.IsCancellationRequested)
    {
        await Task.Delay(Random.Shared.Next(1500, 4000), ct);

        orderNum++;
        var amount = Math.Round(Random.Shared.NextDouble() * 500 + 10, 2);
        var status = Random.Shared.Next(10) < 8 ? "placed" : "cancelled";

        await ctx.Response.WriteAsync($"event: {status}\n", ct);
        await ctx.Response.WriteAsync($"data: Order #{orderNum} — ${amount}\n\n", ct);
        await ctx.Response.Body.FlushAsync(ct);
    }
});

app.MapGet("/events/metrics", async (HttpContext ctx) =>
{
    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

    var ct = ctx.RequestAborted;
    var lastId = 0;

    if (ctx.Request.Headers.TryGetValue("Last-Event-ID", out var lastEventId)
        && int.TryParse(lastEventId, out var parsed))
    {
        lastId = parsed;
    }

    var seq = lastId;
    while (!ct.IsCancellationRequested)
    {
        await Task.Delay(2000, ct);
        seq++;

        var cpu = Math.Round(Random.Shared.NextDouble() * 60 + 10, 1);
        var mem = Random.Shared.Next(40, 85);

        await ctx.Response.WriteAsync($"id: {seq}\n", ct);
        await ctx.Response.WriteAsync($"data: {{\"cpu\":{cpu},\"mem\":{mem},\"seq\":{seq}}}\n\n", ct);
        await ctx.Response.Body.FlushAsync(ct);
    }
});

await app.RunAsync();

static async IAsyncEnumerable<DateTime> StreamClock(
    [EnumeratorCancellation] CancellationToken ct)
{
    while (!ct.IsCancellationRequested)
    {
        yield return DateTime.UtcNow;
        await Task.Delay(1000, ct);
    }
}
