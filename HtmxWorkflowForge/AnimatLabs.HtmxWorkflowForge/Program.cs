using System.Net;
using AnimatLabs.HtmxWorkflowForge.Services;
using AnimatLabs.HtmxWorkflowForge.Workflows;
using WorkflowForge.Loggers;
using WF = WorkflowForge.WorkflowForge;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.File(
    Path.Combine(AppContext.BaseDirectory, "wwwroot", "index.html"),
    "text/html"));

app.MapGet("/workflow/start", async (HttpContext ctx, bool fail = false) =>
{
    ctx.Response.ContentType = "text/event-stream";
    ctx.Response.Headers.CacheControl = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

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

    _ = Task.Run(async () =>
    {
        try
        {
            await smith.ForgeAsync(workflow, foundry, ct).ConfigureAwait(false);
            await SendSseAsync(ctx, "done",
                """<div id="final-status" class="status-success">All steps completed successfully</div>""",
                ct).ConfigureAwait(false);
        }
        catch (OperationCanceledException) { }
        catch
        {
            await SendSseAsync(ctx, "done",
                """<div id="final-status" class="status-failed">Workflow failed &mdash; compensation complete</div>""",
                ct).ConfigureAwait(false);
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
});

await app.RunAsync();

static async Task SendSseAsync(HttpContext ctx, string eventName, string data, CancellationToken ct)
{
    await ctx.Response.WriteAsync($"event: {eventName}\n", ct).ConfigureAwait(false);
    await ctx.Response.WriteAsync($"data: {data}\n\n", ct).ConfigureAwait(false);
    await ctx.Response.Body.FlushAsync(ct).ConfigureAwait(false);
}

static string BuildStepHtml(WorkflowEvent evt)
{
    var icon = evt.Status switch
    {
        "running" => "&#9654;",
        "completed" => "&#10004;",
        "failed" => "&#10008;",
        "compensating" => "&#8634;",
        "compensated" => "&#8634;",
        _ => "&#9679;"
    };

    return $"""<div class="step step-{evt.Status}" id="step-{evt.OperationName}"><span class="step-icon">{icon}</span><span class="step-name">{evt.OperationName}</span><span class="step-detail">{WebUtility.HtmlEncode(evt.Detail)}</span></div>""";
}
