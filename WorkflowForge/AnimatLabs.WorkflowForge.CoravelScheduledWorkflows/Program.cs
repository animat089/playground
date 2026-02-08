using AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Jobs;
using AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Options;
using AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Services;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss ";
});

builder.Services.Configure<ReconciliationJobOptions>(
    builder.Configuration.GetSection(ReconciliationJobOptions.SectionName));

builder.Services.PostConfigure<ReconciliationJobOptions>(options =>
{
    if (options.ScheduleSeconds <= 0)
    {
        options.ScheduleSeconds = 10;
    }
});

builder.Services.AddScheduler();
builder.Services.AddTransient<ReconciliationJob>();

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<IPaymentService, FakePaymentService>();
builder.Services.AddSingleton<IInventoryService, FakeInventoryService>();
builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();

var host = builder.Build();

host.Services.UseScheduler(scheduler =>
{
    var options = host.Services.GetRequiredService<IOptions<ReconciliationJobOptions>>().Value;

    scheduler
        .Schedule<ReconciliationJob>()
        .EverySeconds(options.ScheduleSeconds)
        .PreventOverlapping(nameof(ReconciliationJob));
});

await host.RunAsync();