using System.Threading.Channels;
using AnimatLabs.KafkaSignalR.Hubs;
using AnimatLabs.KafkaSignalR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton(Channel.CreateUnbounded<string>());
builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<EventHub>("/hub/events");

app.MapGet("/sse/events", async (Channel<string> channel, HttpContext ctx, CancellationToken ct) =>
{
    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

    var feature = ctx.Features.Get<Microsoft.AspNetCore.Http.Features.IHttpResponseBodyFeature>();
    feature?.DisableBuffering();

    await foreach (var json in channel.Reader.ReadAllAsync(ct))
    {
        await ctx.Response.WriteAsync($"data: {json}\n\n", ct);
        await ctx.Response.Body.FlushAsync(ct);
    }
});

app.Run();
