using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Microsoft.Extensions.Logging;

namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Services;

public sealed class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendConfirmationAsync(string orderId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sent confirmation email for {OrderId}", orderId);
        return Task.CompletedTask;
    }
}