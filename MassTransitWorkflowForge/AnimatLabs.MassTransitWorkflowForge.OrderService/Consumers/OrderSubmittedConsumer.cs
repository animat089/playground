using AnimatLabs.MassTransitWorkflowForge.Contracts;
using AnimatLabs.MassTransitWorkflowForge.OrderService.Workflows;
using MassTransit;
using WorkflowForge.Loggers;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.MassTransitWorkflowForge.OrderService.Consumers;

public sealed class OrderSubmittedConsumer(IBus bus, ILogger<OrderSubmittedConsumer> logger) : IConsumer<SubmitOrder>
{
    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {
        var order = context.Message;
        logger.LogInformation("Received order {OrderId} for ${Amount}", order.OrderId, order.Amount);

        var shouldFail = order.Amount > 500;
        var workflow = OrderSagaWorkflow.Build(bus, shouldFail);

        using var foundry = WF.CreateFoundry(
            workflowName: workflow.Name,
            initialProperties: new Dictionary<string, object?>
            {
                [SagaKeys.OrderId] = order.OrderId,
                [SagaKeys.Amount] = order.Amount,
                [SagaKeys.CustomerEmail] = order.CustomerEmail
            });

        using var smith = WF.CreateSmith(new ConsoleLogger("WF-Saga"));

        try
        {
            await smith.ForgeAsync(workflow, foundry, context.CancellationToken).ConfigureAwait(false);
            await context.Publish(new OrderAccepted(order.OrderId)).ConfigureAwait(false);
            logger.LogInformation("Order {OrderId} completed successfully", order.OrderId);
        }
        catch (Exception ex)
        {
            await context.Publish(new OrderFailed(order.OrderId, ex.Message)).ConfigureAwait(false);
            logger.LogError(ex, "Order {OrderId} failed -- compensation executed", order.OrderId);
        }
    }
}
