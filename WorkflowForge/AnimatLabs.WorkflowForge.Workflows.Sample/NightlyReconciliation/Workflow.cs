using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using WorkflowForge.Abstractions;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation;

public static class NightlyReconciliationWorkflow
{
    public static IWorkflow Build(
        IOrderRepository orderRepository,
        IPaymentService paymentService,
        IInventoryService inventoryService,
        IEmailSender emailSender)
    {
        return WF
            .CreateWorkflow("NightlyReconciliation")
            .AddOperation(new FetchUnprocessedOrdersOperation(orderRepository))
            .AddOperation(new ProcessPaymentsOperation(paymentService))
            .AddOperation(new UpdateInventoryOperation(inventoryService))
            .AddOperation(new MaybeFailOperation())
            .AddOperation(new SendConfirmationEmailsOperation(emailSender))
            .Build();
    }
}