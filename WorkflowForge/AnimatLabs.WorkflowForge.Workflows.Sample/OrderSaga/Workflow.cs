using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Operations;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;
using WorkflowForge.Abstractions;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga;

public static class OrderSagaWorkflow
{
    public static IWorkflow Build(IMessageBus bus, bool simulatePaymentFailure = false)
    {
        return WF
            .CreateWorkflow("OrderSaga")
            .AddOperation(new ReserveStockStep(bus))
            .AddOperation(new ChargePaymentStep(bus, simulatePaymentFailure))
            .AddOperation(new CreateShipmentStep(bus))
            .Build();
    }
}
