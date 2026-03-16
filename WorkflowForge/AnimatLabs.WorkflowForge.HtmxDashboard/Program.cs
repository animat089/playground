using System.Net;
using AnimatLabs.HtmxWorkflowForge.Services;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using Microsoft.AspNetCore.Http.Features;
using WorkflowForge.Loggers;
using WF = WorkflowForge.WorkflowForge;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
        ctx.Context.Response.Headers.CacheControl = "no-cache, no-store"
});

app.MapGet("/workflow/reset", (bool fail = false) =>
{
    var html = $"""
        <div sse-connect="/workflow/stream?fail={fail.ToString().ToLowerInvariant()}" sse-close="close">
            <div id="steps" sse-swap="step" hx-swap="beforeend"></div>
            <div id="final-status" sse-swap="done" hx-swap="innerHTML"></div>
        </div>
        """;
    return Results.Content(html, "text/html");
});

app.MapGet("/workflow/stream", async (HttpContext ctx, bool fail = false) =>
{
    var bufferingFeature = ctx.Features.Get<IHttpResponseBodyFeature>();
    bufferingFeature?.DisableBuffering();

    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";
    ctx.Response.Headers["X-Accel-Buffering"] = "no";

    var sink = new ChannelEventSink();
    var ct = ctx.RequestAborted;

    var workflow = OrderProcessingWorkflow.Build(sink, fail);

    using var foundry = WF.CreateFoundry(
        workflowName: workflow.Name,
        initialProperties: new Dictionary<string, object?>
        {
            [OrderKeys.ShouldFail] = fail
        });

    using var smith = WF.CreateSmith(new ConsoleLogger("WF"));

    string? finalHtml = null;

    try
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await smith.ForgeAsync(workflow, foundry, ct).ConfigureAwait(false);
                finalHtml = "<p><strong>All steps completed successfully.</strong></p>";
            }
            catch (OperationCanceledException) { }
            catch
            {
                finalHtml = "<p><strong>Workflow failed -- compensation complete.</strong></p>";
            }
            finally
            {
                sink.Complete();
            }
        }, ct);

        await foreach (var evt in sink.Reader.ReadAllAsync(ct))
        {
            var html = BuildStepHtml(evt);
            await SendSseAsync(ctx, "step", html, ct).ConfigureAwait(false);
        }

        if (finalHtml is not null)
        {
            await SendSseAsync(ctx, "done", finalHtml, ct).ConfigureAwait(false);
            await SendSseAsync(ctx, "close", "", ct).ConfigureAwait(false);
        }
    }
    catch (OperationCanceledException) { }
});

await app.RunAsync();

static async Task SendSseAsync(HttpContext ctx, string eventName, string data, CancellationToken ct)
{
    await ctx.Response.WriteAsync($"event: {eventName}\ndata: {data}\n\n", ct).ConfigureAwait(false);
    await ctx.Response.Body.FlushAsync(ct).ConfigureAwait(false);
}

static string BuildStepHtml(WorkflowEvent evt)
{
    var label = evt.Status.ToUpperInvariant();
    return $"""<div class="step">[{label}] <strong>{evt.OperationName}</strong> {WebUtility.HtmlEncode(evt.Detail)}</div>""";
}
