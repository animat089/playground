using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;

public sealed class MaybeFailOperation : WorkflowOperationBase
{
    public override string Name => "MaybeFail";

    protected override Task<object?> ForgeAsyncCore(
        object? inputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var demoFailure = foundry.GetPropertyOrDefault<bool>(ReconciliationKeys.DemoFailure, false);

        if (demoFailure)
        {
            foundry.Logger.LogError("Simulated failure triggered to demonstrate compensation");
            throw new InvalidOperationException("Simulated failure (DemoFailure=true)");
        }

        return Task.FromResult(inputData);
    }

    public override Task RestoreAsync(
        object? outputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
        => Task.CompletedTask;
}