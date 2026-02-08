using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;

public interface IPaymentService
{
    Task<PaymentTransaction> ChargeAsync(Order order, CancellationToken cancellationToken);

    Task RefundAsync(PaymentTransaction transaction, CancellationToken cancellationToken);
}