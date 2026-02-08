namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;

public interface IEmailSender
{
    Task SendConfirmationAsync(string orderId, CancellationToken cancellationToken);
}