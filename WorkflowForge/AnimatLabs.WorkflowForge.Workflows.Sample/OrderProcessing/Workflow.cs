using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing;

public static class OrderProcessingWorkflow
{
    public static IWorkflow Build(IWorkflowEventSink sink, bool shouldFail)
    {
        return WF
            .CreateWorkflow("OrderProcessing")
            .AddOperation(new ValidateOrderOperation(sink))
            .AddOperation(new ReserveStockOperation(sink))
            .AddOperation(new ChargePaymentOperation(sink, shouldFail))
            .AddOperation(new CreateShipmentOperation(sink))
            .AddOperation(new SendNotificationOperation(sink))
            .Build();
    }
}
